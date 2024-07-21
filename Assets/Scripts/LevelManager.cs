using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public DoorController stageOneDoors;
    public DoorController stageTwoDoors;
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerReference pRef;
    private void Awake()
    {
        if(Instance == null)Instance = this;
        else Destroy(this);
    }
    private void Start()
    {
        pRef.SetPc(player);
        SceneManager.LoadScene(2,LoadSceneMode.Additive);
        //player.transform.gameObject.SetActive(true);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMainMenul()
    {
        SceneManager.LoadScene(0);
    }
}
