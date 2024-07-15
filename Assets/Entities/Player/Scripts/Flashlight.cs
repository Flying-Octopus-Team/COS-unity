using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light lightSource;
    private bool currentState;
    private AudioSource audioSource;
    [SerializeField] private AudioClip turnOnSound;
    [SerializeField] private AudioClip turnOffSound;
    [SerializeField] private AudioClip idleSound;
    private void Start()
    {
        lightSource = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();
        currentState = lightSource.enabled;
        audioSource.clip = idleSound;
        audioSource.Play();
        TurnFlashlight(true);
    }
    public void SwitchFlashlight()
    {
        TurnFlashlight(!currentState);
    }
    public void TurnFlashlight(bool newState)
    {
        if(currentState && !newState) 
        {
            audioSource.PlayOneShot(turnOffSound);
            audioSource.Pause();
        }
        if (!currentState && newState) 
        { 
            audioSource.PlayOneShot(turnOnSound);
            audioSource.UnPause();
        }

        currentState = newState;
        lightSource.enabled= currentState;
    }
}
