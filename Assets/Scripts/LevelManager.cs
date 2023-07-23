using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [Header("Overall")]
    [SerializeField] private GameObject[] stages;

    [Header("First Stage")]
    [SerializeField] private MeshRenderer[] indicators;
    private bool[] fuseStates;
    [SerializeField] private Color indicatorsActiveColor;
    [SerializeField] private Color indicatorsInactiveColor;
    [SerializeField] private DoorController stageOneDoors;

    [Header("Second Stage")]
    [SerializeField] private MeshRenderer[] secondIndicators;
    [SerializeField] private List<PCpuzzle> computers;
    [SerializeField] private List<PCpuzzle> choosenComputers;
    [SerializeField] private int codeLen = 4;
    [SerializeField] private int chosenIndex = 0;
    [SerializeField] private DoorController stageTwoDoors;
    bool passedSecondStage = false;
    private void Start()
    {
        if(Instance == null)Instance = this;
        else Destroy(this);

        for(int i = 0; i < indicators.Length; i++)
        {
            indicators[i].material.SetColor("_EmissionColor", indicatorsInactiveColor);
        }
        for (int i = 0; i < secondIndicators.Length; i++)
        {
            secondIndicators[i].material.SetColor("_EmissionColor", indicatorsInactiveColor);
        }

        if(codeLen>= computers.Count)
        {
            codeLen = computers.Count - 1;
            Debug.LogWarning("Code is too long!");
        }
        string newCode = "";
        for (int i=0;i< codeLen; i++)
        {
            int chosen = Random.Range(0, computers.Count);
            newCode += computers[chosen].code;
            choosenComputers.Add(computers[chosen]);
            computers.RemoveAt(chosen);

            
        }

        fuseStates = new bool[indicators.Length];

        stageOneDoors.SwitchpowerState(false);
        stageTwoDoors.SwitchpowerState(false);

        LoadStage(0);
    }
    public void SwitchPowerStageOne(int fuse)
    {
        if (fuse < indicators.Length)
        {
            indicators[fuse].material.SetColor("_EmissionColor", indicatorsActiveColor);
            fuseStates[fuse] = true;
        }

        bool enableDoors = true;

        for(int i = 0; i < fuseStates.Length; i++)
        {
            enableDoors = enableDoors && fuseStates[i];
        }

        if (stageOneDoors)
        {
            stageOneDoors.SwitchpowerState(enableDoors);
        }
    }
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ActivateComputer(PCpuzzle pc)
    {
        if (passedSecondStage) return;
        if(pc == choosenComputers[chosenIndex]) 
        {
            chosenIndex++;
        }
        else
        {
            chosenIndex = 0;
        }

        if(chosenIndex >= codeLen)
        {
            Debug.Log("Passed!");
            stageTwoDoors.SwitchpowerState(true);
            stageTwoDoors.SetState(true);
            passedSecondStage = true;
        }
    }

    public void LoadStage(int stage)
    {
        for (int i = 0; i< stages.Length;i++)
        {
            stages[i].SetActive(stage==i);
        }
    }

    [ContextMenu("LoadLevel1")]
    public void LoadLevel1()
    {
        stages[0].SetActive(true);
    }
    [ContextMenu("LoadLevel2")]
    public void LoadLevel2()
    {
        stages[1].SetActive(true);
    }
    [ContextMenu("LoadLevel3")]
    public void LoadLevel3()
    {
        stages[2].SetActive(true);
    }
}
