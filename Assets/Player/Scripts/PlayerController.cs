using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    private const int MAX_HP = 100;
    [SerializeField] private PlayerReference pRef;
    [Header("Movement")]
    [SerializeField] private float cameraSensitivity = 100f;
    [SerializeField] private float walkingSpeed = 10f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private GameEvent lockCursor;
    [SerializeField] private GameEvent unlockCursor;
    /// <summary>
    /// 
    /// </summary>
    [Header("Latara")]
    public Transform lataraHandler;
    [Header("Sway")]
    [SerializeField] private Vector3 legsOffset;
    private float step = 0.01f;
    private float maxStepDistance = 0.06f;

    private float rotationStep = 4f;
    private float maxRotationStep = 5f;
    public float smooth = 10f;
    private float travelLimit = 0.02f;
    public float multiplier = 0.2f;

    private float speedCurve;
    private float curveSin { get => Mathf.Sin(speedCurve); }
    private float curveCos { get => Mathf.Cos(speedCurve); }
    /// <summary>
    /// 
    /// </summary>
    [Header("Utility")]
    [SerializeField] private LayerMask interactionMask;
    private Flashlight flashlight;
    [SerializeField] private double lifeSupportSwitchTime = 2.0f;
    [SerializeField] private AudioClip lifeSupportOnSound;
    [SerializeField] private AudioClip lifeSupportOffSound;
    [SerializeField] private RectTransform lifesupportBootStatus;
    [SerializeField] private RectTransform lifesupportBootStatusHolder;
    [SerializeField] private Transform gameOverScreen;
    private double lifeSupportHoldTime;
    private bool lifeSupportStatus = true;
    [SerializeField] private int health = 100;
    private float healthLostTimer = 0;

    public float len = 0.3f;

    private Camera playerCamera;
    private Vector2 cameraInput;
    private Vector2 movementInput;
    private Vector3 playerVelocity;
    private Vector3 lastPlayerPosition;

    private float xRot = 0f; //Buffer for mouse up/down input
    private float interactionDistance = 10f;
    public float stepRate = 0.5f;
    private float stepTimer;
    private bool isGrounded;
    private bool canLookAround = true;
    private bool canMoveAround = true;
    private CharacterController cController;

    [Header("Audio")]
    private AudioSource stepSource;
    [SerializeField] private AudioClip[] stepSound;
    [SerializeField] private AudioClip landSound;

    private void Awake()
    {
        pRef.SetPc(this);
    }
    void Start()
    {
        Application.targetFrameRate= 120;
        playerCamera = GetComponentInChildren<Camera>();
        cController = GetComponent<CharacterController>();
        stepSource = GetComponent<AudioSource>();
        flashlight = playerCamera.GetComponentInChildren<Flashlight>();
    }

    private void Update()
    {
        //Landing
        bool newIsGrounded = IsGrounded(); 
        if(!isGrounded && newIsGrounded)
        {
            //wylaczone tymczasowo - nie ma dzwieku upadku
            //stepSource.PlayOneShot(landSound);
        }
        isGrounded= newIsGrounded;
        //
        HandlePlayerLifeSupport();
        HandleCameraMovement();
        HandlePlayerMovement(isGrounded);
        HandlePlayerPhysics(isGrounded);
        FlashLightPositionRotation(isGrounded);
    }


    //MOVEMENT AND INTERACTIONS

    private void HandlePlayerMovement(bool grounded)
    {
        if (!canMoveAround || health <= 0) return;
        float movementPenalty = lifeSupportStatus ? 1 : 0.5f;
        Vector3 moveVector = transform.right * movementInput.x + transform.forward * movementInput.y;
        moveVector *= Time.deltaTime * (walkingSpeed * movementPenalty);

        cController.Move(moveVector);
        if (moveVector.magnitude > 0 && grounded)
        {
            stepTimer += Time.deltaTime * movementPenalty;
            if (stepTimer > stepRate && stepSource && Vector3.Distance(transform.position, lastPlayerPosition)>0.015f)
            {
                stepSource.PlayOneShot(GetRandomClip(ref stepSound));
                stepTimer = 0;
            }
        }
        else
        {
            stepTimer = stepRate*0.85f;
        }
        lastPlayerPosition = transform.position;
    }

    private void HandleCameraMovement()
    {
        if (!canLookAround || health<=0) return;
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
            if(hit.transform.TryGetComponent(out IInteract o))
            {
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green,3);
                o.Interact();
                return;
            }
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow,3);
            return;
        }
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward) * interactionDistance, Color.red,3);
    }

    private void HandlePlayerLifeSupport()
    {
        if (lifeSupportHoldTime > 0f && health>0)
        {
            //effects cuz player holding button
            lifesupportBootStatus.localScale= new Vector3((float)((Time.time - lifeSupportHoldTime)/ lifeSupportSwitchTime),1,1);
            if ((Time.time - lifeSupportHoldTime) >= lifeSupportSwitchTime)
            {
                lifeSupportStatus = !lifeSupportStatus;
                AudioClip sound = (lifeSupportStatus) ? lifeSupportOnSound : lifeSupportOffSound;
                stepSource.PlayOneShot(sound);
                Debug.Log($"Switch life support state to {lifeSupportStatus}");
                lifeSupportHoldTime = 0;
                lifesupportBootStatus.localScale = new Vector3(0, 1, 1);
                lifesupportBootStatusHolder.gameObject.SetActive(false);
            }
        }
        if (!lifeSupportStatus)
        {
            healthLostTimer += Time.deltaTime;
            if(healthLostTimer > 2 ) 
            {
                DamagePlayer(5);
                healthLostTimer = 0;
            }
        }
    }

    public void DamagePlayer(int dmg)
    {
        if (health <= 0) return;
        health-=dmg;
        health=Mathf.Clamp(health, 0, MAX_HP);
        if (health <= 0) PlayerDie();
    }

    void FlashLightPositionRotation(bool grounded)
    {
        if (!canLookAround || health <= 0) return;
        lataraHandler.localPosition = Vector3.Lerp(lataraHandler.localPosition, Sway() + BobOffset(grounded), Time.deltaTime * smooth);
        lataraHandler.localRotation = Quaternion.Slerp(lataraHandler.localRotation, Quaternion.Euler(SwayRotation()) * Quaternion.Euler(BobRotation()), Time.deltaTime * smooth);
    }

    private Vector3 Sway()
    {
        Vector3 invertLook = cameraInput * -step;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);

        return invertLook;
    }
    private Vector3 SwayRotation()
    {
        Vector2 invertLook = cameraInput * -rotationStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);
        return new Vector3(invertLook.y, invertLook.x, invertLook.x);
    }
    private Vector3 BobOffset(bool grounded)
    {
        //horizontal + vertical
        speedCurve += Time.deltaTime * (grounded ? (cameraInput.x + cameraInput.y) * multiplier : 1f) + 0.01f;
        Vector3 bobPosition = new Vector3();
        bobPosition.x = (curveCos * travelLimit * (grounded ? 1 : 0)) - (movementInput.x * travelLimit);
        //vertical
        bobPosition.y = (curveSin * travelLimit) - (cameraInput.y * travelLimit);
        bobPosition.z = -(movementInput.y * travelLimit);

        return bobPosition;
    }

    private Vector3 BobRotation()
    {
        Vector3 bobEulerRotation = new Vector3();
        bobEulerRotation.x = (movementInput != Vector2.zero ? (Mathf.Sin(2 * speedCurve)) : (Mathf.Sin(2 * speedCurve) / 2));
        bobEulerRotation.y = (movementInput != Vector2.zero ?  curveCos : 0);
        bobEulerRotation.z = (movementInput != Vector2.zero ?   curveCos * movementInput.x : 0);
        bobEulerRotation *= multiplier;
        return bobEulerRotation;
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

    public void LockLookingAround(bool lockState)
    {
        canLookAround = lockState;
    }
  
    public void LockMovingAround(bool lockState)
    {
        canMoveAround = lockState;
    }

    public void HealPlayer(int amout)
    {
        health += Mathf.Abs(amout);
        health = Mathf.Clamp(health, 0, MAX_HP);
    }
    public void PlayerDie()
    {
        gameOverScreen.gameObject.SetActive(true);
        unlockCursor.Raise();
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
    
    public void HandleFlashlightInput(InputAction.CallbackContext context)
    {
        if (context.started && flashlight)
        {
            flashlight.SwitchFlashlight();
        }
    }

    public void HandleLifeSupportInput(InputAction.CallbackContext context)
    {
        if (health <= 0) return;
        if (context.performed)
        {
            lifeSupportHoldTime = Time.time;
            lifesupportBootStatus.localScale = new Vector3(0, 1, 1);
            lifesupportBootStatusHolder.gameObject.SetActive(true);
        }
        if (context.canceled)
        {
            lifesupportBootStatus.localScale = new Vector3(0, 1, 1);
            lifesupportBootStatusHolder.gameObject.SetActive(false);
            lifeSupportHoldTime = 0;
        }
    }

    //OTHER
    private Vector3 GetLegsPosition()
    {
        return this.transform.position - legsOffset;
    }
    private AudioClip GetRandomClip(ref AudioClip[] clips)
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
    
    public float GetPlayerDetectionLevel()
    {
        float detection = 1;
        if (lifeSupportStatus) detection *= 3;
        return detection;
    }
    //DEBUG
    [ExecuteAlways]
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = IsGrounded()? Color.green : Color.red;
        Gizmos.DrawSphere(GetLegsPosition(), len);
    }
    private void OnGUI()
    {
        /*
        GUI.Label(new Rect(0, 0, 150, 50), $"PlayerHP: {health}");
        GUI.Label(new Rect(0, 15, 150, 50), $"LifeSupport: {lifeSupportStatus}");
        GUI.Label(new Rect(0, 30, 150, 50), $"canLookAround: {canLookAround}");
        GUI.Label(new Rect(0, 45, 150, 50), $"canMoveAround: {canMoveAround}");
        GUI.Label(new Rect(0, 60, 150, 50), $"Cursor: {Cursor.lockState}");
        */
    }
}
