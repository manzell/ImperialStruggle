using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq; 

public class PlayerAction : BaseAction
{
    public Player player;
    UnityAction callback; 

    protected override void Do(UnityAction callback)
    {   
        this.callback = callback;

        if (Can())
            commands.ForEach(command => command.Do(this));
    }

    public override bool Can() => conditionals.All(c => c.Test(this));

    public bool CanAfford(ActionPoints actionPointCost)
    {
        ActionPoint test; 

        Dictionary<ActionPoint.ActionPointKey, int> availableActionPoints = new Dictionary<ActionPoint.ActionPointKey,int>();

        foreach(ActionPoint ap in player.actionPoints)
        {
            ActionPoint.ActionPointKey apKey = new ActionPoint.ActionPointKey(ap.actionType, ap.actionTier);

            if (availableActionPoints.ContainsKey(apKey))
                availableActionPoints[apKey] += ap.Value(this); 
            else
                availableActionPoints.Add(apKey, ap.Value(this));
        }

        // Note this presently fails to include Major Action Points in affording minor actions
        return actionPointCost.All(cost => availableActionPoints[new ActionPoint.ActionPointKey(cost.actionType, cost.actionTier)] >= cost.Value(this));
    }
}