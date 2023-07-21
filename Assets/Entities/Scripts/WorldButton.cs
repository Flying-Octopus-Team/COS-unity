using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour, IInteract
{
    [SerializeField] private UnityEvent actions;
    [SerializeField] private bool lockable = false;
    [SerializeField] private Material buttonMaterial;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    private bool active = true;
    
    void Start()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer)
        {
            buttonMaterial = renderer.material;
        }
        UdateColor();
    }
    
    public void Interact()
    {
        if(active) 
        {
            actions.Invoke();

            if(lockable)
            {
                Deactivate();
            }
        }

        UdateColor();
    }
    private void UdateColor()
    {
        if (buttonMaterial != null)
        {
            if (active) buttonMaterial.SetColor("_EmissionColor", activeColor);
            else buttonMaterial.SetColor("_EmissionColor", inactiveColor);
        }
        
    }
    public void Deactivate()
    {
        active = false;
    }
}
