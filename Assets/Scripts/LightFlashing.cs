using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlashing : MonoBehaviour {
    public MeshRenderer meshRenderer;
    [Tooltip("0 - light off, 1 - light on")] [SerializeField] private Material[] lightMaterials = new Material[2];
    
    private Light lightComponent;
    private float randomTimeFlashing;
    private bool[] lightOnOrOff = new bool[2] { false, true };

    private void Awake()
    {
        lightComponent = GetComponent<Light>();
    }
    void Start() {
        StartCoroutine(Flashing());
    }

    IEnumerator Flashing() {
        while (true) {
            // i = 0 -> light off, i = 1 -> light on
            for(int i=0; i<2; i++)
            {
                randomTimeFlashing = Random.Range(0.05f, 1f);
                lightComponent.enabled = lightOnOrOff[i];
                meshRenderer.material = lightMaterials[i];
                yield return new WaitForSeconds(randomTimeFlashing);
            }
        }
    }
}
