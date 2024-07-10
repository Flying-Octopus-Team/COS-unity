using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class ButtonScript : MonoBehaviour, IInteract
{
    [SerializeField] private TextMeshProUGUI buttonTMPro;
    [SerializeField] private CodePanelScript addNumber;
    [SerializeField] private Color clickColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private GameObject buttonCube;

    public string buttonValue;
    private AudioSource buttonClickSound;
    private Material cubeMaterial;
    private UnityEvent pressedButton;
    private new MeshRenderer renderer;

    private void Awake()
    {
        renderer = buttonCube.GetComponent<MeshRenderer>();
        buttonClickSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        
        
        if (renderer)
        {
            cubeMaterial = renderer.material;
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
        Invoke("BackToNormalColor", 0.5f);
    }

    // Return to normal color after 0.5 sec
    void BackToNormalColor()
    {
        cubeMaterial.SetColor("_EmissionColor", normalColor);
    }
}
