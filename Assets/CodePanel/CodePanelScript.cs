using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodePanelScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayNumber;
    string text;
    int nr;
    // Start is called before the first frame update
    void Start()
    {
        displayNumber.text = "lalalala";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddNumber(int number)
    {
        Debug.Log(number);
        displayNumber.text = (number.ToString());
        print(displayNumber.text);
    }
}
