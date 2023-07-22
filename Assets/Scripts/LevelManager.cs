using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class LevelManager : MonoBehaviour
{
    [Header("Overall")]
    [SerializeField] private GameObject[] stages;

    [Header("First Stage")]
    [SerializeField] private MeshRenderer[] indicators;
    private bool[] fuseStates;
    [SerializeField] private Color indicatorsActiveColor;
    [SerializeField] private Color indicatorsInactiveColor;
    [SerializeField] private DoorController stageOneDoors;
    private void Start()
    {
        for(int i = 0; i < indicators.Length; i++)
        {
            indicators[i].material.SetColor("_EmissionColor", indicatorsInactiveColor);
        }
        fuseStates = new bool[indicators.Length];
        stageOneDoors.SwitchpowerState(false);
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
