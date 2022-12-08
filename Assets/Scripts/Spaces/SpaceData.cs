using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

public abstract class SpaceData : SerializedScriptableObject
{
    public GameObject prefab;
    public Map map;
    public Faction startingFlag;
    public Phase.Era availableEra;
    public List<SpaceData> adjacentSpaces;
}