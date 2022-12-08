using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

// Neighbor Market Condition is: Does the <Space> in Question have a neighbor that is a market? 
/*
public class NeighborMarketCondition : Conditional
{
    [SerializeField] List<Faction> neighborFactions = new List<Faction>();
    [SerializeField] int numRequired = 1;

    public Conditional.ConditionType ConditionalType => throw new System.NotImplementedException();

    public string ConditionalText => throw new System.NotImplementedException();

    public bool Test(GameAction context) 
    {
        if (context is ActionTarget<Market> targetSpace)
            return targetSpace.target.adjacentSpaces.Count(space => space is Market && neighborFactions.Contains(space.flag)) >= numRequired;
        else
            return true; 
    }
}
*/