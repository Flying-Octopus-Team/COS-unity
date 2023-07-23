using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PCpuzzle : MonoBehaviour, IInteract
{
    public string code;
    private AudioSource audioSource;
    [SerializeField] private TextMeshPro text;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (text) text.SetText(code);
    }
    [ContextMenu("Select PC")]
    public void Interact()
    {
        LevelManager.Instance.ActivateComputer(this);
        if(audioSource) audioSource.Play();
    }

}
