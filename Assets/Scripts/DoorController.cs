using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private bool state;
    [SerializeField] private bool powerState;
    private Animator doorAnim;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private AudioSource doorAS;

    float lockTime = 0;

    private void Start()
    {
        if(doorAnim == null)
        {
            doorAnim = GetComponent<Animator>();
        }
    }
    [ContextMenu("SwitchState")]
    public void SwitchState()
    {
        if (!powerState) return;

        if(Time.time > lockTime)
        {
            state = !state;
            doorAnim.SetBool("DoorState", state);
            if (doorAS) doorAS.PlayOneShot(doorSound);
            lockTime = Time.time + 2;//potem podmienic na faktyczna dlugosc animacji
        }
        
    }
    public void SetState(bool newState)
    {
        if(state != newState) 
        {
            state = newState;
            doorAnim.SetBool("DoorState", state);
            if (doorAS) doorAS.PlayOneShot(doorSound);
        }
    }
    public void SwitchpowerState(bool newState)
    {
        powerState = newState;
    }
    public bool RealDoorState()
    {
        return false;
    }
}
