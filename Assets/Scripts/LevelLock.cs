using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelLock : MonoBehaviour
{
    [SerializeField] private int stageToLoad;
    [SerializeField] private DoorController dorsIn;
    [SerializeField] private DoorController dorsOut;
    [SerializeField] private GameObject blockingWall;

    bool lockedDoors = false;
    bool loaded = false;
    private void Start()
    {
        lockedDoors = false;

        dorsIn.SetState(false);
        dorsOut.SetState(false);

        dorsOut.SwitchpowerState(false);
        dorsIn.SwitchpowerState(false);

        blockingWall.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning(other.name);
        if (!other.CompareTag("Player")) return;
        if (lockedDoors) return;
        lockedDoors = true;
        blockingWall.SetActive(true);
        dorsIn.SetState(false);//zamykamy drzwi wejsciowe
        dorsIn.SwitchpowerState(false);//wylaczamy im zasilanie (blokujemy - gracz juz nie wraca)
    }

    public void LoadNextStage()
    {
        Debug.Log($"Want to load");
        if (!loaded && lockedDoors)
        {
            Debug.Log("Load");
            loaded = true;
            StartCoroutine(LoadNextStageSequence());
        }
    }
    public void ForceLoadNextStage()
    {
        Debug.Log("Forced Load");
        loaded = true;
        StartCoroutine(LoadNextStageSequence());
    }

    private IEnumerator LoadNextStageSequence()
    {
        AsyncOperation asyncUnLoad = SceneManager.UnloadSceneAsync(stageToLoad-1);
        while (!asyncUnLoad.isDone)
        {
            yield return null;
        }
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(stageToLoad, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        dorsOut.SwitchpowerState(true);//zasilamy drzwi wyjsciowe
        dorsOut.SetState(true);//otwieramy drzwi wyjsciowe

    }
}
