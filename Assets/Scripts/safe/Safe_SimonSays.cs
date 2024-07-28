using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Ten kod ma za zadanie obsługiwać logikę sejfu.
/// Na początku generuje on kod, który pozwoli go otworzyć, a potem sprawdza, czy gracz poprawnie 
/// owy kod wpisuje oraz reaguje na jego poziom poprawności.
/// </summary>

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
    [SerializeField] private Material RedButtonsMaterial;
    [SerializeField] private Material YellowButtonsMaterial;
    [SerializeField] private Material GreenButtonsMaterial;
    [SerializeField] private Material BlueButtonsMaterial;
    [SerializeField] public AudioSource buttonClickedAudio;
    [SerializeField] public AudioSource levelFailedAudio;
    [SerializeField] public AudioSource levelPassedAudio;
    [SerializeField] public AudioSource safeOpeningAudio;
    [SerializeField] public Animator animator;
    private string[] arrayOfButtons = {"blue", "green", "red", "yellow"};
    private string[] sequenceArray = {"pass", "pass", "pass", "pass", "pass"};
    private bool beginThePuzzle;
    private bool waitForLevelsEnd = true;
    private bool levelPassed = false;
    private bool gameOver = false;
    private int levelOfGame = 1;
    private int sequencesMoment = 0;
    ///private float movement = 0.005f;
    
    void Start(){
        //Funkcja inicjacyjna ustawia sekwencję kodu. Kod się już nie zmieni.
        //a także zmienia kolor przycisków na szary "wyłączone".
        ChangeAllButtonsColor(ColorOfInactiveButton);
        

        for(int i = 0; i < sequenceArray.Length; i++){
            System.Random rd = new System.Random();
            int random = rd.Next(0, 4);
            sequenceArray[i] = arrayOfButtons[random];
            UnityEngine.Debug.Log(sequenceArray[i] + " " + random);
        }     
    }
    void Update(){
        //Sprawdza czy można rozpocząć "grę"
        //Sprawdza też, czy gra się skończyła.
        if (beginThePuzzle){
            if(levelPassed) levelOfGame++;

            if(levelOfGame == 6 && !gameOver){ 
                StartCoroutine(AudioPlayer(safeOpeningAudio));
                animator.SetTrigger("TrOpen");
                gameOver = true;
            } else if(!waitForLevelsEnd){
                UnityEngine.Debug.Log(waitForLevelsEnd);

                StartCoroutine(showLevelsSequence(levelOfGame));
                waitForLevelsEnd = true;
                UnityEngine.Debug.Log(waitForLevelsEnd);
                sequencesMoment = 0;
            }

            levelPassed = false;
        }
    }

    public void Interact(){
        //Jeśli gracz wejdzie w interakcję z sejfem, pozwala rozpocząć "grę" za pomocą funkcji "TouchButtons()",
        //jeśli wcześniej nie została zainicjowana.
        if(!beginThePuzzle){
            StartCoroutine(TouchButtons(false));
        }
    }

    public void ButtonClicked(string color){
        //Tak samo jak funkcja "Interact()", pozwala rozpocząć "grę", ale jeżeli została już rozpoczęta
        //wywołuje funkcję "TouchButton()", która sprawdza poprawność wprowadzanego kodu. 
        if(levelOfGame != 6){
            if(!beginThePuzzle){
                StartCoroutine(TouchButtons(false));
            }
            else if(waitForLevelsEnd){
                StartCoroutine(TouchButton(color));
                StartCoroutine(AudioPlayer(buttonClickedAudio));
            }
        } 
    }

    public void OnApplicationQuit(){
        //Zmienia kolor przycisków na ich odpowiedniki na zakończenie, żeby lepiej dla wygody programisty 
        ChangeAllButtonsColor(Color.black, true);
    }

    public void ChangeAllButtonsColor(Color color, bool showColors = false){
        //Pozwala zmienić kolor przycisków.
        if(showColors){

            RedButtonsMaterial.SetColor("_EmissionColor", ColorOfRedButton);
            YellowButtonsMaterial.SetColor("_EmissionColor", ColorOfYellowButton);
            GreenButtonsMaterial.SetColor("_EmissionColor", ColorOfGreenButton);
            BlueButtonsMaterial.SetColor("_EmissionColor", ColorOfBlueButton);

        } else {

            RedButtonsMaterial.SetColor("_EmissionColor", color);
            YellowButtonsMaterial.SetColor("_EmissionColor", color);
            GreenButtonsMaterial.SetColor("_EmissionColor", color);
            BlueButtonsMaterial.SetColor("_EmissionColor", color);

        }

    }

    public void ChangeButtonsColor(string color){
        //Zaświeca dane przyciski, jeśli poprawne.
        //Jeśli gracz wybierze zły przycisk, przyciski zaświecą się na czerwono i 
        if(color == sequenceArray[sequencesMoment] || !waitForLevelsEnd){
            switch(color){
                case "blue":{
                    BlueButtonsMaterial.SetColor("_EmissionColor", ColorOfBlueButton);
                    break;
                }
                case "green":{
                    GreenButtonsMaterial.SetColor("_EmissionColor", ColorOfGreenButton);
                    break;
                }
                case "red":{
                    RedButtonsMaterial.SetColor("_EmissionColor", ColorOfRedButton);
                    break;
                }
                case "yellow":{
                    YellowButtonsMaterial.SetColor("_EmissionColor", ColorOfYellowButton);
                    break;
                }
            }
            if(waitForLevelsEnd) sequencesMoment++;
        } else {
            StartCoroutine(TouchButtons(false));
        }
        UnityEngine.Debug.Log(sequencesMoment + " " + levelOfGame);
        if (sequencesMoment == levelOfGame){
            StartCoroutine(TouchButtons(true));
        } 
    }

    private IEnumerator TouchButton(string color){
        //Zmienia kolor przycisku i po chwili go wyłącza.
        ChangeButtonsColor(color);
        yield return new WaitForSeconds(0.33f);
            
        ChangeAllButtonsColor(ColorOfInactiveButton);
    }

    private IEnumerator TouchButtons(bool isCorrect){
        //Zmienia kolor przycisków i po chwili je wyłącza.

        if(!beginThePuzzle){
            ChangeAllButtonsColor(Color.black, true);
        } else if(isCorrect){
            ChangeAllButtonsColor(ColorOfGreenButton);
            StartCoroutine(AudioPlayer(levelPassedAudio));
            levelPassed = true;
        } else {
            ChangeAllButtonsColor(ColorOfRedButton);
            StartCoroutine(AudioPlayer(levelFailedAudio));
        }
        yield return new WaitForSeconds(0.33f);

        if(beginThePuzzle){
            waitForLevelsEnd = false;
        }else if(isCorrect){
            levelPassed = true;
        } else {
            waitForLevelsEnd = false;
        }
        ChangeAllButtonsColor(ColorOfInactiveButton);
        
        beginThePuzzle = true;
    }

    private IEnumerator showLevelsSequence(int howMuch){
        //Kod wywoływany w celu ukazania prawidłowej sekwencji.
        if(levelOfGame != 6) for(int i = 0; i < howMuch; i++){
            switch(sequenceArray[i]){
                case "blue":{
                    BlueButtonsMaterial.SetColor("_EmissionColor", ColorOfBlueButton);
                    break;
                }
                case "green":{
                    GreenButtonsMaterial.SetColor("_EmissionColor", ColorOfGreenButton);
                    break;
                }
                case "red":{
                    RedButtonsMaterial.SetColor("_EmissionColor", ColorOfRedButton);
                    break;
                }
                case "yellow":{
                    YellowButtonsMaterial.SetColor("_EmissionColor", ColorOfYellowButton);
                    break;
                }
            }

            yield return new WaitForSeconds(0.33f);

            ChangeAllButtonsColor(ColorOfInactiveButton);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator AudioPlayer(AudioSource audio){
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
    }
}
