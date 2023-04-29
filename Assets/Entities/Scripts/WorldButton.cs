using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour, IInteract
{
    [SerializeField] private UnityEvent actions;
    public void Interact()
    {
        actions.Invoke();
    }
}
