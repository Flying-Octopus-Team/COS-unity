using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMigmig : MonoBehaviour {
    public MeshRenderer meshRenderer;
    public Material on;
    public Material off;

    void Start() {
        StartCoroutine(Migmig());
    }

    IEnumerator Migmig() {
        while (true) {
            float randomTimeMigingOff = Random.Range(0.05f, 1f);
            float randomTimeMigingOn = Random.Range(0.05f, 1f);

            GetComponent<Light>().enabled = false;
            meshRenderer.material = off;
            yield return new WaitForSeconds(randomTimeMigingOff);
            GetComponent<Light>().enabled = true;
            meshRenderer.material = on;
            yield return new WaitForSeconds(randomTimeMigingOn);
        }
    }
}
