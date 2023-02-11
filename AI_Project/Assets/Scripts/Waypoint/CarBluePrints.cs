using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarBluePrint
{
    [Header("Setup")]
    public float carSpeed;
    public float trackerSpeed;
    public float rotateSpeed;
    public GameObject carObj;
    public float overDistance;
    public float overTrackerDistance;

    [Header("Do not Touch")]
    public int currentWP = 0;
    public GameObject tracker;
}