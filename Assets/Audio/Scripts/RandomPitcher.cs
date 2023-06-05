using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomPitcher : MonoBehaviour
{
    private AudioSource source;
    [Header("Pitch values")]
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;
    [Header("Change time")]
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private float transitionTime;
    void Start()
    {
        source = GetComponent<AudioSource>();
        if(source == null) { Debug.LogWarning($"BRAKUJE AUDIO SOURCE W {gameObject.name}!"); }
        StartCoroutine(PitchLoop());
    }

    IEnumerator PitchLoop()
    {
        while (true)
        {
            float wantedPitch = Random.Range(minPitch, maxPitch);
            float startedPitch = source.pitch;
            float timer = 0;
            while (timer <= transitionTime)
            {
                source.pitch = Mathf.Lerp(startedPitch, wantedPitch, (timer / transitionTime));
                timer += Time.deltaTime;
                yield return null;
            } 
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }
}
