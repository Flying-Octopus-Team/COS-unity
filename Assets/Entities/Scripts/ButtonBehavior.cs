using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour, IInteract{
    private bool isClicked = false;
    private float movement = 0.005f;
    [SerializeField] public float buttonsSpeed = 0.0005f;
    void Start(){
        
    }

    public void Interact(){
        Debug.Log("Witam w mojej kuchni!");
        isClicked = true;
    }

    void Update(){
        if(isClicked){
            Click();
        }
    }

    void Click(){
        transform.Translate(new Vector3(0,0,buttonsSpeed));
    }
}
