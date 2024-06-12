using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SejfSzymonMówi : MonoBehaviour, IInteract
{
    [SerializeField] private UnityEngine.Object Player;
    [SerializeField] private Color ColorOfInactiveButton;
    [SerializeField] private Color ColorOfRedButton;
    [SerializeField] private Color ColorOfYellowButton;
    [SerializeField] private Color ColorOfGreenButton;
    [SerializeField] private Color ColorOfBlueButton;
    [SerializeField] private UnityEngine.Object RedButton;
    [SerializeField] private UnityEngine.Object YellowButton;
    [SerializeField] private UnityEngine.Object GreenButton;
    [SerializeField] private UnityEngine.Object BlueButton;
    [SerializeField] private Material RedButtonsMaterial;
    [SerializeField] private Material YellowButtonsMaterial;
    [SerializeField] private Material GreenButtonsMaterial;
    [SerializeField] private Material BlueButtonsMaterial;
    private bool beginThePuzzle;
    
    void Start()
    {
        RedButtonsMaterial.SetColor("_EmissionColor", ColorOfInactiveButton);
        YellowButtonsMaterial.SetColor("_EmissionColor", ColorOfInactiveButton);
        GreenButtonsMaterial.SetColor("_EmissionColor", ColorOfInactiveButton);
        BlueButtonsMaterial.SetColor("_EmissionColor", ColorOfInactiveButton);
        
    }

    // Update is called once per frame
    void Update(){
        if (beginThePuzzle){
            RedButtonsMaterial.SetColor("_EmissionColor", ColorOfRedButton);
            YellowButtonsMaterial.SetColor("_EmissionColor", ColorOfYellowButton);
            GreenButtonsMaterial.SetColor("_EmissionColor", ColorOfGreenButton);
            BlueButtonsMaterial.SetColor("_EmissionColor", ColorOfBlueButton);
        }
    }

    public void Interact(){
        beginThePuzzle = true;
    }

    public void OnApplicationQuit(){
        RedButtonsMaterial.SetColor("_EmissionColor", ColorOfRedButton);
        YellowButtonsMaterial.SetColor("_EmissionColor", ColorOfYellowButton);
        GreenButtonsMaterial.SetColor("_EmissionColor", ColorOfGreenButton);
        BlueButtonsMaterial.SetColor("_EmissionColor", ColorOfBlueButton);
    }
}
