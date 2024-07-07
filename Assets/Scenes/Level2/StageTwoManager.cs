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
    [SerializeField] private List<PCpuzzle> computers;
    [SerializeField] private List<PCpuzzle> choosenComputers;
    [SerializeField] private int codeLen = 4;
    private int chosenIndex = 0;
    [SerializeField] private DoorController stageTwoDoors;
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
            //secondIndicators[i].material.SetColor("_EmissionColor", indicatorsInactiveColor);
        }

        if (codeLen >= computers.Count)
        {
            codeLen = computers.Count - 1;
            Debug.LogWarning("Code is too long!");
        }

        string newCode = "";
        for (int i = 0; i < codeLen; i++)
        {
            int chosen = Random.Range(0, computers.Count);
            newCode += computers[chosen].code;
            choosenComputers.Add(computers[chosen]);
            computers.RemoveAt(chosen);
        }
        if (codeOutput != null) codeOutput.SetText(newCode);

        stageTwoDoors.SwitchpowerState(false);
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
            Debug.Log("Passed!");
            stageTwoDoors.SwitchpowerState(true);
            stageTwoDoors.SetState(true);
            passedSecondStage = true;
        }

        for (int i = 0; i < secondIndicators.Length; i++)
        {
            secondIndicators[i].material.SetColor("_EmissionColor", (i < chosenIndex) ? indicatorsActiveColor : indicatorsInactiveColor);
        }
    }
}
