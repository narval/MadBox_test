﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="levelx", menuName ="Data/LevelData")]
public class LevelData : ScriptableObject
{
    public Vector3[] waypoints;
}
