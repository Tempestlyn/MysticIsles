using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IslandBlock : MonoBehaviour
{
    public Vector2 position;
    public List<Vector2> FilledCoords;
    public int BlockID;
}
