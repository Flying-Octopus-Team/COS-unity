using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IPOI
{
    public string POIName { get; set; }
    public string inAnimation { get; set; }
    public string outAnimation { get; set; }
    public void InEvent();
    public void OutEvent();

}

