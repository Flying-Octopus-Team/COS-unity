using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour, IInteract
{
    [Tooltip("0 - light off, 1 - light on")][SerializeField] private Material[] lightMaterials = new Material[2];

    private Light lightComponent;
    private MeshRenderer meshRenderer;
    [SerializeField] private GameObject gameObjectMesh;
    [SerializeField] private GameObject gameObjectLight;
    bool isPowered = true;

    void Start()
    {
        // At the start, the light is off
        lightComponent.enabled = false;
        meshRenderer.material = lightMaterials[0];
    }

    private void Awake()
    {
        meshRenderer = gameObjectMesh.GetComponent<MeshRenderer>();
        lightComponent = gameObjectLight.GetComponent<Light>();
    }

    public void Interact()
    {
        if (isPowered)
        {
            lightComponent.enabled = !lightComponent.enabled;
            if (lightComponent.enabled) meshRenderer.material = lightMaterials[1];
            else meshRenderer.material = lightMaterials[0];
        }
    }

    public void SwtichPowerState(bool state)
    {
        isPowered = state;
        if(!isPowered)
        {
            lightComponent.enabled = false;
            meshRenderer.material = lightMaterials[0];
        }
        else
        {
            lightComponent.enabled = true;
            meshRenderer.material = lightMaterials[1];
        }
    }
}