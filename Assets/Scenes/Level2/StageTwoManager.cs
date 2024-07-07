using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageTwoManager : MonoBehaviour
{
    public static StageTwoManager Instance; 

    [SerializeField] private Color indicatorsActiveColor;
    [SerializeField] private Color indicatorsInactiveColor;

    [Header("Second Stage")]
    [SerializeField] private TextMeshPro codeOutput;
    [SerializeField] private MeshRenderer[] secondIndicators;
    private PCpuzzle[] computers;
    private PCpuzzle[] choosenComputers;
    [SerializeField] private int codeLen = 4;
    private int chosenIndex = 0;
    bool passedSecondStage = false;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    void Start()
    {
        for (int i = 0; i < secondIndicators.Length; i++)
        {
            secondIndicators[i].material.SetColor("_EmissionColor", indicatorsInactiveColor);
        }
        computers = GameObject.FindObjectsOfType<PCpuzzle>();
        if (codeLen >= computers.Length)
        {
            codeLen = computers.Length - 1;
            Debug.LogWarning("Code is too long!");
        }

        string newCode = "";
        choosenComputers = new PCpuzzle[codeLen];
        for (int i = 0; i < codeLen; i++)
        {
            int chosen = Random.Range(0, computers.Length);
            newCode += computers[chosen].code;
            choosenComputers[i]=computers[chosen];
            //computers.RemoveAt(chosen);
        }
        if (codeOutput != null) codeOutput.SetText(newCode);


        LevelManager.Instance.stageTwoDoors.SwitchpowerState(false);
    }

    public void ActivateComputer(PCpuzzle pc)
    {
        if (passedSecondStage) return;
        if (pc == choosenComputers[chosenIndex])
        {
            chosenIndex++;
        }
        else
        {
            chosenIndex = 0;
        }
        if (chosenIndex >= codeLen)
        {
            PassLevel();
        }

        for (int i = 0; i < secondIndicators.Length; i++)
        {
            secondIndicators[i].material.SetColor("_EmissionColor", (i < chosenIndex) ? indicatorsActiveColor : indicatorsInactiveColor);
        }
    }

    [ContextMenu("UnlockDoors")]
    public void PassLevel()
    {
        Debug.Log("Passed!");
        LevelManager.Instance.stageTwoDoors.SwitchpowerState(true);
        LevelManager.Instance.stageTwoDoors.SetState(true);
        passedSecondStage = true;
    }
}
