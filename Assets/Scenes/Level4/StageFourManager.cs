using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFourManager : MonoBehaviour
{
    [SerializeField] private Enemy COS;
    void Start()
    {
        COS.gameObject.SetActive(false);
    }
    private IEnumerator EntryCutscene()
    {
        yield return new WaitForSeconds(3);
        COS.gameObject.SetActive(true);
    }
}
