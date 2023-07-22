using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLock : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private DoorController dorsIn;
    [SerializeField] private DoorController dorsOut;

    bool locked = false;    

    private void Start()
    {
        dorsOut.SwitchpowerState(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && locked==false)
        {
            StartCoroutine(LoadSequence());
        }
    }

    IEnumerator LoadSequence()
    {
        locked = true;
        dorsIn.SetState(false);//zamykamy drzwi wejsciowe
        dorsIn.SwitchpowerState(false);//wylaczamy im zasilanie (blokujemy - gracz juz nie wraca)
        //odladuj stary poziom
        //Laduj nowy poziom
        yield return new WaitForSeconds (2);
        dorsOut.SwitchpowerState(true);//zasilamy drzwi wyjsciowe
        dorsOut.SetState(true);//otwieramy drzwi wejsciowe
    }
}
