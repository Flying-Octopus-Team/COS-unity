using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelLock : MonoBehaviour
{
    [SerializeField] private int stageToLoad;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private DoorController dorsIn;
    [SerializeField] private DoorController dorsOut;
    [SerializeField] private UnityEvent eventToRise;

    bool locked = false;
    bool playerIn = false;

    private void Start()
    {
        dorsOut.SwitchpowerState(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = true;
            if (locked == false)
            {
                StartCoroutine(LoadSequence());
            }
            else
            {
                Debug.LogWarning("Przestan skakac no");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = false;
        }
    }

    IEnumerator LoadSequence()
    {
        locked = true;
        dorsIn.SetState(false);//zamykamy drzwi wejsciowe
        dorsIn.SwitchpowerState(false);//wylaczamy im zasilanie (blokujemy - gracz juz nie wraca)
        eventToRise.Invoke();//glownie chodzi o to by ograniczyc ruch gracza

        yield return new WaitForSeconds (3);//czekaj az drzwi sie zamkno

        //if (playerIn) //czy przypadkiem nie wyskoczyl urwis jeden
       // {
            dorsOut.SwitchpowerState(true);//zasilamy drzwi wyjsciowe
            dorsOut.SetState(true);//otwieramy drzwi wyjsciowe
            if (levelManager) levelManager.LoadStage(stageToLoad);//zaladuj tylko odpowiedni etap
        //}
        /*
        else //jesli jednak wyskoczyl to otwieramy wejscie jeszcze raz
        {
            dorsIn.SetState(true);
            dorsIn.SwitchpowerState(true);
            locked = false; //odblokowujemy znowu mozliwosc wejscia w przejscie
        }
        */
    }
}
