using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waypoint
{
    [Range(0, 1)]
    public float percentage;
    public Vector3 position;
}
