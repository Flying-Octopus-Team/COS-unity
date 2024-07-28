using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject credits;

    private void Start()
    {
        if (PlayerPrefs.GetInt("PlayCredits") > 0)
        {
            PlayerPrefs.SetInt("PlayCredits", 0);
            mainMenu.SetActive(false);
            credits.SetActive(true);
        }
    }
}
