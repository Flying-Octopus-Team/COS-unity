using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOneManager : MonoBehaviour
{

    [SerializeField] private Color indicatorsActiveColor;
    [SerializeField] private Color indicatorsInactiveColor;
    
    [SerializeField] private MeshRenderer[] indicators;
    private bool[] fuseStates;
    
    private void Start()
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].material.SetColor("_EmissionColor", indicatorsInactiveColor);
        }

        fuseStates = new bool[indicators.Length];
        LevelManager.Instance.stageOneDoors.SwitchpowerState(false);
    }
    public void SwitchPowerStageOne(int fuse)
    {
        
        if (fuse < indicators.Length)
        {
            indicators[fuse].material.SetColor("_EmissionColor", indicatorsActiveColor);
            fuseStates[fuse] = true;
        }
        
        bool enableDoors = true;

        for (int i = 0; i < fuseStates.Length; i++)
        {
            enableDoors = enableDoors && fuseStates[i];
        }

        if (LevelManager.Instance.stageOneDoors)
        {
            LevelManager.Instance.stageOneDoors.SwitchpowerState(enableDoors);
            LevelManager.Instance.stageOneDoors.SetState(true);
        }
    }
    

    [ContextMenu("UnlockDoors")]
    public void UnlockAllDors()
    {
        LevelManager.Instance.stageOneDoors.SwitchpowerState(true);
        LevelManager.Instance.stageOneDoors.SetState(true);
    }
}
