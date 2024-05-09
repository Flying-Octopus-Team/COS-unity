using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlashing : MonoBehaviour {
    public MeshRenderer meshRenderer;
    [Tooltip("0 - light off, 1 - light on")] [SerializeField] private Material[] lightMaterials = new Material[2];
    
    private Light lightComponent;
    private float randomTimeFlashing;

    private void Awake()
    {
        lightComponent = GetComponent<Light>();
    }
    void Start() {
        // Draw first value
        randomTimeFlashing = Random.Range(0.15f, 1f);
    }

    private void FixedUpdate()
    {
        if(Time.time >= randomTimeFlashing)
        {
            lightComponent.enabled = !lightComponent.enabled;
            if (lightComponent.enabled) meshRenderer.material = lightMaterials[1];
            else meshRenderer.material = lightMaterials[0];
            randomTimeFlashing += Random.Range(0.05f, 1f);
        }
    }

}
