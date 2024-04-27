using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Events;
using UnityEngine.Audio;

public class CodePanelScript : MonoBehaviour
{
    [SerializeField] private string code;
    [SerializeField] private int codeLength;
    [SerializeField] private TextMeshProUGUI displayNumber;
    [SerializeField] private GameObject display;
    [SerializeField] private UnityEvent actionAfterGoodCode;

    [Header("Colors")]
    [SerializeField] private Color goodColor;
    [SerializeField] private Color badColor;
    [SerializeField] private Color normalColor;
  
    [Header("Audios")]
    [SerializeField] private AudioClip goodCodeAudio;
    [SerializeField] private AudioClip badCodeAudio;

    private Material displayNumberMaterial;
    private AudioSource enteredCodeAudio;

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = display.GetComponent<MeshRenderer>();
        enteredCodeAudio = GetComponent<AudioSource>();
        if (renderer)
        {
            displayNumberMaterial = renderer.material;
            print(displayNumberMaterial);
        }
        displayNumber.text = "";
    }

    public void AddNumber(string number)
    {
        // Add number to display 
        if(number != "E" && number != "C" && displayNumber.text.Length < code.Length)
        {
            displayNumber.text += number;
        } 
        // Clear display
        else if(number == "C")
        {
            displayNumber.text = "";
            displayNumberMaterial.SetColor("_EmissionColor", normalColor);
        }
        // Enter Code
        else if(number == "E")
        {
            // If good set good color, good audio and call action
            if (displayNumber.text == code)
            {
                displayNumberMaterial.SetColor("_EmissionColor", goodColor);
                actionAfterGoodCode.Invoke();
                enteredCodeAudio.clip = goodCodeAudio;
                enteredCodeAudio.Play();
            }
            // If not set bad color and bad audio
            else
            {
                displayNumberMaterial.SetColor("_EmissionColor", badColor);
                enteredCodeAudio.clip = badCodeAudio;
                enteredCodeAudio.Play();
            }
        }   
          
    }
}
