using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

// Neighbor Market Condition is: Does the <Space> in Question have a neighbor that is a market? 
public class NeighborMarketCondition : Conditional
{
    [SerializeField] List<Game.Faction> neighborFactions = new List<Game.Faction>();
    [SerializeField] int numRequired = 1; 

    public override bool Test(BaseAction context) 
    {
        if (context is ITargetType<Market> targetSpace)
            return targetSpace.target.adjacentSpaces.Count(space => space is Market && neighborFactions.Contains(space.flag)) >= numRequired;
        else
            return true; 
    }
}
