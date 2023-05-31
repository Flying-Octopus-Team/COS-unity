using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsOcclusion : MonoBehaviour {
    [SerializeField] private AudioLowPassFilter[] lowPassFilter;

    private void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Player")) {
            foreach (AudioLowPassFilter filter in lowPassFilter) {
                filter.cutoffFrequency = 22000;
            }
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.CompareTag("Player")) {
            foreach (AudioLowPassFilter filter in lowPassFilter) {
                filter.cutoffFrequency = 1000;
            }
        }
    }
}
