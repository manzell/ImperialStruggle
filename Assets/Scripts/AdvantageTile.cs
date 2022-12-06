using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class AdvantageTile : MonoBehaviour
{
    public enum AdvantageTileState { Ready, Exhaused }
    public AdvantageTileState tileState { get; private set; }
    public List<Space> adjacentSpaces = new();

    public Faction faction
    {
        get
        {
            if (adjacentSpaces.All(space => space.flag == adjacentSpaces[0].flag))
                return adjacentSpaces[0].flag;
            return null; 
        }
    }
}
