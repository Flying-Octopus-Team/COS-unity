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
    [Header("Debug")]
    [SerializeField] private int levelToStart;
    [SerializeField] private Transform[] doorsPoints;
    [SerializeField] private Transform[] spawns;
    [SerializeField] private int levelToLoad;
    private void Awake()
    {
        if(Instance == null)Instance = this;
        else Destroy(this);
    }
    private void Start()
    {
        pRef.SetPc(player);
        SceneManager.LoadScene(2 + levelToStart, LoadSceneMode.Additive);
        if(levelToStart > 0)
        {
            player.transform.position = spawns[levelToStart-1].position;
            player.transform.gameObject.SetActive(true);
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMainMenul()
    {
        SceneManager.LoadScene(0);
    }
    [ContextMenu("Jump to level")]
    public void DebugGoToLevel()
    {
        if (levelToLoad >= doorsPoints.Length) return;
        
        player.transform.position = doorsPoints[levelToLoad].position;
        doorsPoints[levelToLoad].transform.parent.GetComponent<LevelLock>().ForceLoadNextStage();

    }
}
