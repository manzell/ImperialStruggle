using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class NeighborMarketCondition : Conditional
{
    [SerializeField] List<Game.Faction> neighborFactions = new List<Game.Faction>();

    public override bool Test(Object space)
    {
        if (space is Space)
            return (space as Space).adjacentSpaces.Any(market => market is Market && neighborFactions.Contains(market.flag));

        return true; 
    }
}
