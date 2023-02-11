using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCar : MonoBehaviour
{
    protected enum WheelsPosition
    {
        FrontLeft,
        FrontRight,
        RearLeft,
        RearRight
    }

    [Serializable]
    protected struct Wheels
    {
        public Transform WheelModel;
        public WheelCollider WheelCollider;

        public WheelsPosition WheelsPosition;
    }
}

