using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEvents : MonoBehaviour {
    public static CatEvents current;

    private void Awake() {
        current = this;
    }

    public event Action<Vector3> onCatMove;
    public void CatMove(Vector3 destination) {
        if (onCatMove != null) {
            onCatMove(destination);
        }
    }
}
