using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField] private PlayerReference pRef;
    [SerializeField] private GameEvent lockPlayerMovement;
    [SerializeField] private GameEvent unlockPlayerMovement;
    [SerializeField] private UnityEvent initialeventToRise;
    [SerializeField] private UnityEvent eventToRise;
    [SerializeField] private PlayerReference playerReference;
    bool semaphoreLock = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.LogWarning("EndingSequence!");
        if (!semaphoreLock && pRef.GetPc())
        {
            semaphoreLock = true;
            StartCoroutine(EndingSequence());
        }
    }
    IEnumerator EndingSequence()
    {
        initialeventToRise.Invoke();
        lockPlayerMovement.Raise();
        yield return new WaitForSeconds(1);
        eventToRise.Invoke();
        yield return new WaitForSeconds(1);
        unlockPlayerMovement.Raise();
    }

}
