using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class ButtonSrcipt : MonoBehaviour, IInteract
{
    [SerializeField] public string buttonValue;
    [SerializeField] private TextMeshProUGUI buttonTMPro;
    [SerializeField] private CodePanelScript addNumber;
    [SerializeField] private Color clickColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private GameObject buttonCube;
    
    private AudioSource buttonClickSound;
    private Material cubeMaterial;
    private UnityEvent pressedButton;

    void Start()
    {
        MeshRenderer renderer = buttonCube.GetComponent<MeshRenderer>();
        buttonClickSound = GetComponent<AudioSource>();
        
        if (renderer)
        {
            cubeMaterial = renderer.material;
            print(cubeMaterial);
        }

        if (pressedButton == null)
        {
            pressedButton = new UnityEvent();
        }

        // Add method to call after Interact
        pressedButton.AddListener(OnPressedButton);
        pressedButton.AddListener(ChangeColor);

        buttonTMPro.text = buttonValue;

    }

    // After click do CodePanel's method addNumber 
    void OnPressedButton()
    {
        addNumber.AddNumber(buttonValue);
    }

    // After interact player with button call methods (Listener)
    public void Interact()
    {
        pressedButton.Invoke();
    }

    // Change Color and play audio after click
    void ChangeColor()
    {
        cubeMaterial.SetColor("_EmissionColor", clickColor);
        if(buttonValue != "E") buttonClickSound.Play();
        StartCoroutine(waiter());
    }

    // Retururn to normal color after 0.5 sec
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.5f);
        cubeMaterial.SetColor("_EmissionColor", normalColor);
    }
}
