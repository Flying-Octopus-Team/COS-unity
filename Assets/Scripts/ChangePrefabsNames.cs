using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ChangePrefabsNames : MonoBehaviour {
    private List<GameObject> prefabs = new List<GameObject>();

    [ContextMenu("Look For Prefabs")]
    private void LookForPrefabs() {
        prefabs.Clear();

        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        Debug.Log($"{allObjects.Count()} GameObjects found on the scene");

        for (int i = 0; i < allObjects.Length; i++) {
            if (PrefabUtility.GetPrefabInstanceStatus(allObjects[i]) == PrefabInstanceStatus.Connected) {
                prefabs.Add(allObjects[i]);
            }
        }

        Debug.Log($"{prefabs.Count()} Prefabs found on the scene");
    }

    [ContextMenu("Rename instances of prefabs to its names")]
    void RenamePrefabs() {
        for (int i = 0; i < prefabs.Count; i++) {
            //Debug.Log(PrefabUtility.GetPrefabParent(prefabs[1]));
            Debug.Log(PrefabUtility.GetCorrespondingObjectFromOriginalSource(prefabs[i]));
            prefabs[i].name = PrefabUtility.GetCorrespondingObjectFromOriginalSource(prefabs[i]).name;
        }
    }

}
