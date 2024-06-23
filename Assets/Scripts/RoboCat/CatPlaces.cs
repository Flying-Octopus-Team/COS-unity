using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CatPlaces {
    [SerializeField] private AnimationClip animation;
    [SerializeField] private GameObject destinationObject;

    public AnimationClip GetAnimationClip() {
        return animation;
    }

    public void SetAnimationClip(AnimationClip a) {
        animation = a;
    }

    public GameObject GetDestination() {
        return destinationObject;
    }

    public void SetDestination(GameObject d) {
        destinationObject = d;
    }
}
