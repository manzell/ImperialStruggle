using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpaceData : ScriptableObject
{
    public string spaceName; 
    public Map map;
    public Game.Era availableEra;
    public List<Space> adjacentSpaces;
    public int flagCost;
    public bool prestige, alliance;
}
