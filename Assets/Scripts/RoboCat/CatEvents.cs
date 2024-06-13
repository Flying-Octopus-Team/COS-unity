using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEvents : MonoBehaviour {
    public static CatEvents current;

    private void Awake() {
        current = this;
    }

    public event Action onCatMove;
    public void CatMove() {
        if (onCatMove != null) {
            onCatMove();
        }
    }
}
