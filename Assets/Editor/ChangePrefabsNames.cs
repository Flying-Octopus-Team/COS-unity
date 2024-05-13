using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ChangePrefabsNames : Editor {
    public static List<GameObject> prefabs = new List<GameObject>();

    [MenuItem("Prefabs Settings/Look for prefabs")]
    public static void LookForPrefabs() {
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

    [MenuItem("Prefabs Settings/Rename prefabs")]
    public static void RenamePrefabs() {
        for (int i = 0; i < prefabs.Count; i++) {
            //Debug.Log(PrefabUtility.GetPrefabParent(prefabs[1]));
            Debug.Log(PrefabUtility.GetCorrespondingObjectFromOriginalSource(prefabs[i]));
            prefabs[i].name = PrefabUtility.GetCorrespondingObjectFromOriginalSource(prefabs[i]).name;
        }
    }

}
