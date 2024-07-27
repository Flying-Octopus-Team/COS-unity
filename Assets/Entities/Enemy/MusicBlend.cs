using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBlend : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<BlendSound> soundsToBlend;
    [SerializeField] private PlayerReference referenceToPlayer;
    [SerializeField] private float volume = 1;
    private Transform playerTransform;
    private void Start()
    {
        foreach (BlendSound bs in soundsToBlend)
        {
            bs.audioSource = Instantiate(audioSource, this.transform);
            bs.audioSource.volume = 0;
            bs.audioSource.clip = bs.clip;
            bs.audioSource.Play();
        }
        playerTransform = referenceToPlayer.GetPc().transform;
    }

    private void Update()
    {
        float range = Vector3.Distance(transform.position, playerTransform.position);
        Debug.Log($"{range}");
        bool founded = false;
        for(int i = soundsToBlend.Count-1; i>=0;i--)
        {
            if (range <= soundsToBlend[i].range && founded==false)
            {
                Debug.Log($"{range} <= {soundsToBlend[i].range}");
                soundsToBlend[i].audioSource.volume = this.volume;
                founded = true;
            }
            else
            {
                soundsToBlend[i].audioSource.volume = 0;
            }
        }
    }

    public void DisableDelayed(int time)
    {
        StartCoroutine(DelayInDisable(time));
    }
    private IEnumerator DelayInDisable(int time)
    {
        yield return new WaitForSeconds(time);
        this.enabled = false;
    }
}
[System.Serializable]
public class BlendSound
{
    public float range;
    public AudioClip clip;
    public AudioSource audioSource;
}