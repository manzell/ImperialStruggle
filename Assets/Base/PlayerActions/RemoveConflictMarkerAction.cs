using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class RemoveConflictMarkerAction : PlayerAction, ITargetType<Space>
{
    public Space target => GetComponent<Space>();

    public override bool Can()
    {
        if(actionPointCost.Count == 0)
        {
            // The basic cost is 2 Minor Mil, or 1 if the Space is Protected 
            ActionPoint ap = new ActionPoint(ActionPoint.ActionType.Military, ActionPoint.ActionTier.Minor);

            if (target is Market market && market.protectedMarket)
                ap.baseValue = 1;
            else
                ap.baseValue = 2;

            actionPointCost.Add(ap); 
        }

        return base.Can();
    }
}
