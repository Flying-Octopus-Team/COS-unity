using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscenes : MonoBehaviour
{
    [SerializeField] private PlayerReference playerRef;
    [SerializeField] private Camera cam;
    public void OnStartCutsceneEnd()
    {
        if(playerRef == null) return;
        if(playerRef.GetPc() == null) return;

        playerRef.GetPc().gameObject.SetActive(true);
        cam.gameObject.SetActive(false);
    }
}
