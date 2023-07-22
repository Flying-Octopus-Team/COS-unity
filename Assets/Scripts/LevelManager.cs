using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class LevelManager : MonoBehaviour
{
    [Header("Overall")]
    [SerializeField] private GameObject[] levels;

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

        Debug.Log(enableDoors);
        if (stageOneDoors)
        {
            stageOneDoors.SwitchpowerState(true);
        }
    }
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    [ContextMenu("LoadLevel1")]
    public void LoadLevel1()
    {
        levels[0].SetActive(true);
    }
    [ContextMenu("LoadLevel2")]
    public void LoadLevel2()
    {
        levels[1].SetActive(true);
    }
    [ContextMenu("LoadLevel3")]
    public void LoadLevel3()
    {
        levels[2].SetActive(true);
    }
}
