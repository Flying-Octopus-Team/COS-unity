using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{
    [SerializeField] private bool state;
    [SerializeField] private bool powerState;
    private Animator doorAnim;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private AudioSource doorAS;
    [SerializeField] private WorldButton doorButton;
    [SerializeField] private UnityEvent eventOnStartOpening;

    float lockTime = 0;

    private void Start()
    {
        if(doorAnim == null)
        {
            doorAnim = GetComponent<Animator>();
        }
        SwitchpowerState(powerState);
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
        if (doorButton)
        {
            doorButton.ChangeButtonState(powerState);
        }
    }
    public bool RealDoorState()
    {
        return false;
    }

    public void AnimationStartEvent()
    {
        eventOnStartOpening.Invoke();
    }
}
