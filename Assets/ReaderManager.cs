using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaderManager : MonoBehaviour
{
    public static ReaderManager instance;
    private AudioSource playDialogue;
    private Queue<AudioClip> playQueue = new Queue<AudioClip>();
    private bool isCoroutine = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playDialogue = GetComponent<AudioSource>();
    }

    public void PlayDialogue(AudioClip dialogueAudio, bool canBePlayed)
    {
        if (canBePlayed)
        {
            if (!playDialogue.isPlaying)
            {
                playDialogue.clip = dialogueAudio;
                playDialogue.Play();
            }
            else
            {
                playQueue.Enqueue(dialogueAudio);
                if (!isCoroutine) StartCoroutine(PlayAudioQueue());
            }
        }
    }

    IEnumerator PlayAudioQueue()
    {
        isCoroutine = true;
        while(playQueue.Count > 0) {
            while (playDialogue.isPlaying) yield return new WaitForSeconds(1.0f);
            playDialogue.clip = playQueue.Dequeue();
            playDialogue.Play();
        }
        isCoroutine = false;
    }
}
