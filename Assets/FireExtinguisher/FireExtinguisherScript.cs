using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireExtinguisherScript : MonoBehaviour, IInteract
{ 
    
    private Rigidbody rb;
    private float endTime = 0;
    private bool isInteract = false;
    public void Interact()
    {
        if(rb == null) return;
        
        if (!isInteract)
        {
            StartCoroutine(Fly(2f));
            rb.isKinematic = false;
            isInteract = true;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Fly(float howLong)
    {
        endTime = Time.time + howLong;
        while (Time.time <= endTime)
        {
            rb.AddForce(Vector3.up * 0.1f, ForceMode.Impulse);
            yield return null;
        }
    }

}
