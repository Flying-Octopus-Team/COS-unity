using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField] private PlayerReference pRef;
    [SerializeField] private GameEvent lockPlayerMovement;
    [SerializeField] private GameEvent unlockCursor;
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
        lockPlayerMovement.Raise();
        yield return new WaitForSeconds(3);
        unlockCursor.Raise();
        SceneManager.LoadScene(0);
    }
}
