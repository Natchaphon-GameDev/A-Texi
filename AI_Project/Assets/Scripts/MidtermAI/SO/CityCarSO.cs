using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Car/CityCar Stat")]
public class CityCarSO : ScriptableObject
{
    [Header("Setup Speed")]
    public float carSpeed = 10f;
    public float trackerSpeed = 20f;
    public float rotateSpeed = 1f;
    
    [Header("Setup Distance")]
    public float overDistance = 1f;
    public float overTrackerDistance = 5f;
}
