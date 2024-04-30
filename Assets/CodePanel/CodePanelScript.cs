using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Audio;

public class CodePanelScript : MonoBehaviour
{
    [SerializeField] private string code;
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
    private new MeshRenderer renderer;

    void Awake()
    {
        renderer = display.GetComponent<MeshRenderer>();
        enteredCodeAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (renderer)
        {
            displayNumberMaterial = renderer.material;
            print(displayNumberMaterial);
        }
        displayNumber.SetText("");
    }

    public void AddNumber(string number)
    {
        // Add number to display 
        
       switch(number)
        {
            case "C":
                displayNumber.SetText("");
                displayNumberMaterial.SetColor("_EmissionColor", normalColor);
                break;
            case "E":
                // If good set good color, good audio and call action
                if (displayNumber.text == code)
                {
                    displayNumberMaterial.SetColor("_EmissionColor", goodColor);
                    actionAfterGoodCode.Invoke();
                    enteredCodeAudio.clip = goodCodeAudio;
                    enteredCodeAudio.Play();
                    return;
                }
                // If not set bad color and bad audio
                displayNumberMaterial.SetColor("_EmissionColor", badColor);
                enteredCodeAudio.clip = badCodeAudio;
                enteredCodeAudio.Play();
                break;
            default:
                if(displayNumber.text.Length < code.Length)
                {
                    displayNumber.text += number;
                }
                break;
        }
       
    }
}
