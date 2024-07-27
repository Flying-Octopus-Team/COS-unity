using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAppear : MonoBehaviour {
    [SerializeField] private AudioClip catMeow;
    private Cat cat;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        cat = GetComponent<Cat>();
    }

    public void CatJumpIn() {
        cat.enabled = true;
        audioSource.clip = catMeow;
        audioSource.Play();
    }
}
