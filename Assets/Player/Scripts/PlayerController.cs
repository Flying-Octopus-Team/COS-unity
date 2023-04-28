using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float cameraSensitivity = 100f;
    [SerializeField] private float walkingSpeed = 10f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask interactionMask;
    [SerializeField] private Flashlight flashlight;

    public float len = 0.4f;

    private Camera playerCamera;
    private Vector2 cameraInput;
    private Vector2 movementInput;
    private Vector3 playerVelocity;

    private float xRot = 0f; //Buffer for mouse up/down input
    private float interactionDistance = 10f;
    public float stepRate = 0.5f;
    private float stepTimer;
    private bool isGrounded;
    private CharacterController cController;

    [Header("Audio")]
    private AudioSource stepSource;
    [SerializeField] private AudioClip[] stepSound;
    [SerializeField] private AudioClip landSound;

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        cController = GetComponent<CharacterController>();
        stepSource = GetComponent<AudioSource>();
        flashlight = playerCamera.GetComponentInChildren<Flashlight>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        bool newIsGrounded = IsGrounded(); 
        if(!isGrounded && newIsGrounded)
        {
            stepSource.PlayOneShot(landSound);
        }
        isGrounded= newIsGrounded;
        HandleCameraMovement();
        HandlePlayerMovement(isGrounded);
        HandlePlayerPhysics(isGrounded);
    }


    //MOVEMENT AND INTERACTIONS

    private void HandlePlayerMovement(bool grounded)
    {
        Vector3 moveVector = transform.right * movementInput.x + transform.forward * movementInput.y;
        moveVector *= Time.deltaTime * walkingSpeed;

        cController.Move(moveVector);

        if (moveVector.magnitude > 0 && grounded)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer > stepRate && stepSource)
            {
                stepSource.PlayOneShot(GetRandomClip(ref stepSound));
                stepTimer = 0;
            }
        }
        else
        {
            stepTimer = stepRate*0.85f;
        }
    }

    private void HandleCameraMovement()
    {
        Vector2 cameraMovement = cameraInput * cameraSensitivity * Time.deltaTime;
        this.transform.Rotate(Vector3.up * cameraMovement.x);

        xRot -= cameraMovement.y;//zmienic na + dla odroconej osi
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }

    private void HandlePlayerPhysics(bool grounded)
    {
        if (!grounded)
        {
            playerVelocity += (Physics.gravity * Time.deltaTime) * Time.deltaTime;
        }
        else
        {
            playerVelocity.y = 0;
        }
        cController.Move(playerVelocity);
    }
    private void HandlePlayerInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, 
            playerCamera.transform.forward, out hit, interactionDistance, interactionMask))
        {
            if(hit.transform.TryGetComponent<IInteract>(out IInteract o))
            {
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green,3);
                o.Interact();
                return;
            }
            if (flashlight) flashlight.SwitchFlashlight();
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow,3);
            return;
        }

        if (flashlight) flashlight.SwitchFlashlight();
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward) * interactionDistance, Color.red,3);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)return;
        if (hit.moveDirection.y < -0.3)return;
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * 2;
    }
    
    private bool IsGrounded()
    {        
        //Moze potem zmienic na box?
        return Physics.CheckSphere(GetLegsPosition(), len, groundMask);
    }

    //INPUT
    public void HandleLookupInput(InputAction.CallbackContext context)
    {
        cameraInput = context.ReadValue<Vector2>();
    }

    public void HandleMovementInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void HandleInteractInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            HandlePlayerInteraction();
        }
    }
    //OTHER
    private Vector3 GetLegsPosition()
    {
        return this.transform.position - Vector3.up;
    }
    private AudioClip GetRandomClip(ref AudioClip[] clips)
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
    //DEBUG
    [ExecuteAlways]
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = IsGrounded()? Color.green : Color.red;
        Gizmos.DrawSphere(GetLegsPosition(), len);
    }
}
