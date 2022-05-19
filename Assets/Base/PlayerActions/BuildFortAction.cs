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
        if (baseActionCost == null)
        {
            baseActionCost = new ActionPoint();
            baseActionCost.actionType = ActionPoint.ActionType.Military;
            baseActionCost.actionTier = ActionPoint.ActionTier.Minor;
        }

        baseActionCost.actionPoints = target.flagCost;

        if (!actionPointCost.Contains(baseActionCost))
            actionPointCost.Add(baseActionCost); 
    }
}
