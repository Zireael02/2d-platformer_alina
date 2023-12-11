using System;
using UnityEngine;

namespace Platformer
{
    [Serializable]
    public struct AIConfig
    {
        public float Speed;
        public float MinDistanceToTarget;
        public Transform[] Waypoints;
    }
}