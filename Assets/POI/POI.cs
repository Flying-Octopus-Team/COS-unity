using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class POI : MonoBehaviour, IPOI
{
    [SerializeField] private UnityEvent inEvent;
    [SerializeField] private UnityEvent outEvent;

    [SerializeField] string inAnimationName;
    [SerializeField] string outAnimationName;
    [SerializeField] string namePOI;

    public string POIName
    {
        get { return namePOI;  }
        set { namePOI = value; }
    }

    public string inAnimation
    {
        get { return inAnimationName; }
        set { inAnimationName = value; }
    }

    public string outAnimation
    {
        get { return outAnimationName; }
        set { outAnimationName = value;  }
    }

    public void InEvent ()
    {
        inEvent.Invoke();
    }

    public void OutEvent()
    {
        outEvent.Invoke();
    }
}
