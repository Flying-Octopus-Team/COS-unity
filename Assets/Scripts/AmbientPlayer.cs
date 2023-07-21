using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.InputSystem.Utilities;

[RequireComponent(typeof(AudioSource))]
public class AmbientPlayer : MonoBehaviour
{
    [SerializeField] private float fadeTime = 1f;
    private AudioSource audioSource;
    private float audioVolume;
    //public bool fade = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioVolume = audioSource.volume;   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeIn());
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        audioSource.Play();
        float timer = 0;
        while (timer <= fadeTime)
        {
            audioSource.volume = Mathf.Lerp(0, audioVolume, (timer / fadeTime));
            timer += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = audioVolume;
    }
    IEnumerator FadeOut()
    {
        float timer = 0;
        while (timer <= fadeTime)
        {
            audioSource.volume = Mathf.Lerp(audioVolume, 0, (timer / fadeTime));
            timer += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 0;
        audioSource.Stop();
    }
}
