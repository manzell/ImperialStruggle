using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour, ICriteria, ISelectable
{
    public Map map; 
    public Game.Faction flag; 
    public Game.Era availableEra;
    public List<Space> adjacentSpaces;
    public int flagCost;
    public bool prestige, alliance, conflictMarker; 
}
