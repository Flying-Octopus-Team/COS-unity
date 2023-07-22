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

    private void Start()
    {
        if(doorAnim == null)
        {
            doorAnim = GetComponent<Animator>();
        }
    }
    public void SwitchState()
    {
        if (!powerState) return;

        state = !state;
        doorAnim.SetBool("DoorState",state);
        if(doorAS) doorAS.PlayOneShot(doorSound);
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
}
