using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class StageFourManager : MonoBehaviour
{
    [SerializeField] private PlayerReference reference;
    [SerializeField] private Enemy COS;
    int fuzes = 0;
    bool lockedFuzes = false;
    [SerializeField] private Color indicatorsActiveColor;
    [SerializeField] private Color indicatorsInactiveColor;

    [SerializeField] private MeshRenderer[] indicators;
    [SerializeField] private AudioSource endSound;
    [SerializeField] private AudioClip wziumClip;
    [SerializeField] private AudioClip blehClip;


    [SerializeField] private UnityEvent onFinishedFuzes;
    [SerializeField] private UnityEvent onFinishedCutscene;
    void Start()
    {
        COS.gameObject.SetActive(false);
        StartCoroutine(EntryCutscene());
        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].material.SetColor("_EmissionColor", indicatorsInactiveColor);
        }
    }
    private IEnumerator EntryCutscene()
    {
        yield return new WaitForSeconds(3);
        COS.gameObject.SetActive(true);
    }
    public void ActiveFuse()
    {
        fuzes++;

        for(int i = 0; i < indicators.Length; i++)
        {
            indicators[i].material.SetColor("_EmissionColor", i < fuzes? indicatorsActiveColor: indicatorsInactiveColor);
        }
        if(fuzes >= 3 && lockedFuzes==false)
        {
            lockedFuzes = true;
            onFinishedFuzes.Invoke();
        }
    }
    public void StartEndingSequence()
    {
        StartCoroutine(EndingSequence());
    }

    private IEnumerator EndingSequence()
    {
        PlayerPrefs.SetInt("PlayCredits", 1);
        PlayerController pc = reference.GetPc();
        float f = 0;
        while (f <= 1)
        {
            f += Time.deltaTime/5;
            pc.SetOpacity(f);
            Debug.Log(f);
            yield return null;
        }
        endSound.clip = wziumClip;
        endSound.Play();
        yield return new WaitForSeconds(wziumClip.length + 2);
        endSound.clip = blehClip;
        endSound.Play();
        yield return new WaitForSeconds(blehClip.length + 1);
        onFinishedCutscene.Invoke();
        SceneManager.LoadScene(0);
    }
}
