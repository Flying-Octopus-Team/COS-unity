using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class FireExtinguisherScript : MonoBehaviour, IInteract
{ 

    [Tooltip("Thrust force")][SerializeField] private float upForce = 0.1f;
    [Tooltip("Side Thrust magnitude")][SerializeField] private float sideForce = 0.1f;
    [Tooltip("Time up")][SerializeField] private float flyTime = 2.0f;

    private bool isInteract = false;
    private float endTime = 0;
    private Rigidbody rb;
    private ParticleSystem feParticleSystem;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        feParticleSystem = GetComponent<ParticleSystem>();
    }
    public void Interact()
    {
        if (rb == null) return;
        if (!isInteract)
        {
            StartCoroutine(Fly(flyTime));
            rb.isKinematic = false;
            isInteract = true;
        }
    }

    IEnumerator Fly(float howLong)
    {
        endTime = Time.time + howLong;
        rb.AddTorque(new Vector3(Random.Range(0.5f, 2f), Random.Range(0.5f, 2f), Random.Range(0.5f, 2f)));
        while (Time.time <= endTime)
        {
            rb.AddForce(this.transform.up * upForce + this.transform.right * MathF.Cos(Time.time*15)*sideForce, ForceMode.Impulse);
            yield return null;
        }
        feParticleSystem.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            isInteract = false;
        }
    }

}