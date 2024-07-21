using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioCutScene : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] GameObject particleGameObject;
    AudioSource audioCutScene;
    ParticleSystem particleSystemCutScene;

    private void Awake()
    {
        audioCutScene = GetComponent<AudioSource>();
        particleSystemCutScene = particleGameObject.GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        particleSystemCutScene.Stop();
    }

    public void PlayAudio()
    {
        audioCutScene.clip = audioClip;
        audioCutScene.Play();
    }

    public void PlayParticles()
    {
        particleSystemCutScene.Play();
        particleGameObject.SetActive(false);
    }

    public void SetParticlesTrue()
    {
        particleGameObject.SetActive(true);
    }

    public void StopAudio()
    {
        audioCutScene.Stop(); 
    }

    public void StopParticles()
    {
        particleSystemCutScene.Stop();
    }

}
