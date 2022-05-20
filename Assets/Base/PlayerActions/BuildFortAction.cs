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
            baseActionCost = new ActionPoint();
            baseActionCost.actionType = ActionPoint.ActionType.Military;
            baseActionCost.actionTier = ActionPoint.ActionTier.Minor;
            actionPointCost.Add(baseActionCost);
        }

        baseActionCost.actionPoints = target.flagCost;
    }
}
