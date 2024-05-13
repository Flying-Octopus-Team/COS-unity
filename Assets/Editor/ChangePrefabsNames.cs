using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangePrefabsNames : Editor {
    private static List<GameObject> prefabs = new List<GameObject>();
    private static Scene activeScene;

    [MenuItem("Prefabs Settings/Look for prefabs")]
    public static void LookForPrefabs() {
        prefabs.Clear();
        activeScene = SceneManager.GetActiveScene();

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
        if (prefabs.Count == 0) {
            Debug.LogError("No prefabs found on the scene! Make sure you run \"Look for prefabs\" before running \"Rename prefabs\"");
            return;
        }

        if (activeScene != SceneManager.GetActiveScene()) {
            prefabs.Clear();
            RenamePrefabs();
        }

        for (int i = 0; i < prefabs.Count; i++) {
            if (prefabs[i] != null) {
                Debug.Log(PrefabUtility.GetCorrespondingObjectFromOriginalSource(prefabs[i]));
                prefabs[i].name = PrefabUtility.GetCorrespondingObjectFromOriginalSource(prefabs[i]).name;
            }
        }
    }

}
