using System;
using System.Collections.Generic;
using UnityEngine;

    public class TargetDestinationSetup : MonoBehaviour
    {
        public List<SetTarget> targetPos;

        [Serializable]
        public struct SetTarget
        {
            public Destination destination;
            public Transform pos;
        }
    }