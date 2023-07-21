using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour, IInteract
{
    [SerializeField] private UnityEvent actions;
    private bool active = true;
    public void Interact()
    {
        if(active) 
        {
            actions.Invoke();
        }
    }
    public void Deactivate()
    {
        active = false;
    }
}
