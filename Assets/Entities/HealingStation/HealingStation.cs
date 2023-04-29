using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingStation : MonoBehaviour
{
    [SerializeField] private PlayerReference pRef;
    [SerializeField] private GameEvent lockPlayerMovement;
    [SerializeField] private GameEvent unlockPlayerMovement;
    bool semaphoreLock = false;

    public void StartHealSequence()
    {
        if(!semaphoreLock && pRef.GetPc())
        {
            semaphoreLock = true;
            StartCoroutine(HealSequence());
        }
        
    }
    IEnumerator HealSequence()
    {
        lockPlayerMovement.Raise();
        for (int i=0;i<2;i++)
        {
            pRef.GetPc().HealPlayer(5);
            yield return new WaitForSeconds(1);
        }
        unlockPlayerMovement.Raise();
        semaphoreLock = false;
    }
}
