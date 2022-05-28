using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildFortAction : PlayerAction, ITargetType<Fort>
{
    public Fort target => GetComponent<Fort>();
    ActionPoint baseActionCost;

    void Awake()
    {
        if (actionPointCost.Count > 0)
            baseActionCost = actionPointCost[0];
        else if (baseActionCost == null)
        {
            baseActionCost = new ActionPoint(ActionPoint.ActionType.Military, ActionPoint.ActionTier.Minor);
            actionPointCost.Add(baseActionCost);
        }

        baseActionCost.baseValue = target.flagCost;
    }
}
