using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairFortAction : PlayerAction
{
    ActionPoint baseActionCost;

    /*
    public override bool Can()
    {
        Fort fort = GetComponent<Fort>();

        if (actionPointCost.Count > 0)
            baseActionCost = actionPointCost[0];
        else if (baseActionCost == null)
        {
            baseActionCost = new ActionPoint(ActionPoint.ActionType.Military, ActionPoint.ActionTier.Minor);
            actionPointCost.Add(baseActionCost);
        }

        baseActionCost.baseValue = fort.flagCost;

        if (fort.flag == player.faction)
            baseActionCost.baseValue -= 1; 
        else if(fort.flag != Game.Neutral)
            baseActionCost.baseValue += 1;

        return base.Can();
    }
    */

    protected override void Do()
    {
        throw new System.NotImplementedException();
    }
}
