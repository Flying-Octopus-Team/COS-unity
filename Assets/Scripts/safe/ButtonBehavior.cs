using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jedyną rolą tego skryptu jest wywoływanie funkcji znajdującej się w Obiekcie "SejfSzymonMówi", gdy gracz go naciśnie.

public class ButtonBehavior : MonoBehaviour, IInteract{
    [SerializeField] public string color;
    SejfSzymonMówi safe;
    void Start(){
        safe = GameObject.FindGameObjectWithTag("Safe").GetComponent<SejfSzymonMówi>();
    }

    public void Interact(){
        safe.ButtonClicked(color);
    }
}
