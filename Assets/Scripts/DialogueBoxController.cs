using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxController : MonoBehaviour
{
    [SerializeField] AudioClip dialougeAudio;
    bool canBePlayed = true;
    private void OnTriggerEnter(Collider other)
    {
        ReaderManager.instance.PlayDialogue(dialougeAudio, canBePlayed);
        canBePlayed = false;
    }
}
