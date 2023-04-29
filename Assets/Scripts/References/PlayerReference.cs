using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRef", menuName = "ScriptableObjects/PlayerRef")]
public class PlayerReference : ScriptableObject
{
    private PlayerController pc;

    public void SetPc( PlayerController p)
    {
        pc = p;
    }
    public PlayerController GetPc()
    {
        if (pc == null) Debug.LogWarning("Trying get acces to null player!");
        return pc;
    }
}
