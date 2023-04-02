using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataItemTile",menuName = "Dcrawl/DataItemTile")]
public class DataItemTile : ScriptableObject
{
    public GameObject tilePrefab;
    public bool Solid = false;
}
