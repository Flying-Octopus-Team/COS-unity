using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMigmig : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Migmig());
    }

    IEnumerator Migmig() {
        while (true) {
            float randomTimeMigingOff = Random.Range(0.05f, 1f);
            float randomTimeMigingOn = Random.Range(0.05f, 1f);

            GetComponent<Light>().enabled = false;
            yield return new WaitForSeconds(randomTimeMigingOff);
            GetComponent<Light>().enabled = true;
            yield return new WaitForSeconds(randomTimeMigingOn);
        }
    }
}
