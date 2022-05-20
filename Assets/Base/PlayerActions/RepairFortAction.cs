using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairFortAction : PlayerAction
{
    ActionPoint baseActionCost; 

    public override bool Can()
    {
        Fort fort = GetComponent<Fort>();

        if (actionPointCost.Count > 0)
            baseActionCost = actionPointCost[0];
        else if (baseActionCost == null)
        {
            baseActionCost = new ActionPoint();
            baseActionCost.actionType = ActionPoint.ActionType.Military;
            baseActionCost.actionTier = ActionPoint.ActionTier.Minor;
            actionPointCost.Add(baseActionCost);
        }

        baseActionCost.actionPoints = fort.flagCost;

        if (fort.flag == player.faction)
            baseActionCost.actionPoints -= 1; 
        else if(fort.flag != Game.Faction.Neutral)
            baseActionCost.actionPoints += 1;

        return base.Can();
    }
}
