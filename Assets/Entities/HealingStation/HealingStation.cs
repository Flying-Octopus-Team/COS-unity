using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingStation : MonoBehaviour
{
    [SerializeField] private GameEvent lockPlayerMovement;
    [SerializeField] private GameEvent unlockPlayerMovement;
    bool semaphoreLock = false;

    public void StartHealSequence()
    {
        if(!semaphoreLock)
        {
            semaphoreLock = true;
            StartCoroutine(HealSequence());
        }
        
    }
    IEnumerator HealSequence()
    {
        lockPlayerMovement.Raise();
        yield return new WaitForSeconds(2);
        unlockPlayerMovement.Raise();
        semaphoreLock = false;
    }
}
