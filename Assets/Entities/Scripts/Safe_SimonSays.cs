using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SejfSzymonMówi : MonoBehaviour, IInteract
{
    [SerializeField] private Color ColorOfInactiveButton;
    [SerializeField] private Color ColorOfRedButton;
    [SerializeField] private Color ColorOfYellowButton;
    [SerializeField] private Color ColorOfGreenButton;
    [SerializeField] private Color ColorOfBlueButton;
    [SerializeField] private UnityEngine.Object RedButton;
    [SerializeField] private UnityEngine.Object YellowButton;
    [SerializeField] private UnityEngine.Object GreenButton;
    [SerializeField] private UnityEngine.Object BlueButton;
    [SerializeField] private Material RedButtonsColor;
    [SerializeField] private Material YellowButtonsColor;
    [SerializeField] private Material GreenButtonsColor;
    [SerializeField] private Material BlueButtonsColor;
    
    void Start()
    {
        RedButtonsColor.SetColor("_EmissionColor", ColorOfInactiveButton);
        YellowButtonsColor.SetColor("_EmissionColor", ColorOfInactiveButton);
        GreenButtonsColor.SetColor("_EmissionColor", ColorOfInactiveButton);
        BlueButtonsColor.SetColor("_EmissionColor", ColorOfInactiveButton);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(){

    }
}
