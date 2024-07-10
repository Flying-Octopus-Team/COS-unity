using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlashing : MonoBehaviour {
   
    [Tooltip("0 - light off, 1 - light on")] [SerializeField] private Material[] lightMaterials = new Material[2];
    
    private Light lightComponent;
    private float randomTimeFlashing;
    private MeshRenderer meshRenderer;
    [SerializeField] private GameObject gameObjectMesh;
    [SerializeField] private GameObject gameObjectLight;

    private void Awake()
    {
        if(gameObjectMesh!=null)gameObjectMesh.TryGetComponent(out meshRenderer);
        if (gameObjectLight != null) gameObjectLight.TryGetComponent(out lightComponent);
    }
    void Start() {
        // Draw first value
        randomTimeFlashing = Random.Range(0.15f, 1f);
    }

    private void FixedUpdate()
    {
        if(Time.time >= randomTimeFlashing)
        {
            if(lightComponent != null)
            {
                lightComponent.enabled = !lightComponent.enabled;
                if(meshRenderer != null)
                {
                    if (lightComponent.enabled) meshRenderer.material = lightMaterials[1];
                    else meshRenderer.material = lightMaterials[0];
                }  
            }

            randomTimeFlashing += Random.Range(0.05f, 1f);
        }
    }

}
