using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class ButtonSrcipt : MonoBehaviour
{
    [SerializeField] public int buttonValue;
    [SerializeField] private TextMeshProUGUI buttonTMPro;
    private CodePanelScript addNumber = new CodePanelScript();
    private BoxCollider buttonBoxCollider;

    UnityEvent pressedButton;

    void Start()
    {
        buttonTMPro.text = buttonValue.ToString();

        if (pressedButton == null)
        {
            pressedButton = new UnityEvent();
        }

        pressedButton.AddListener(testuje);

    }

    // Update is called once per frame
    void Update()
    {

    }


    // It allows using a cube as a button
    private void OnMouseUpAsButton()
    {
        pressedButton.Invoke();
    }

    void testuje()
    {
        addNumber.AddNumber(buttonValue);
    }
}
