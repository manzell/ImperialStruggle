using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class AdvantageTile : MonoBehaviour
{
    public List<Space> adjacentSpaces = new List<Space>();
    public bool exhausted = false; 

    public Game.Faction faction
    {
        get
        {
            if (adjacentSpaces.All(space => space.flag == adjacentSpaces[0].flag))
                return adjacentSpaces[0].flag;
            return Game.Faction.Neutral; 
        }
    }
}
