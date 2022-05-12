using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class ShiftSpaceAction : PlayerAction
{
    public ActionPoints actionPointCost; // This is essentially a Modifier cost, since our actual cost and cost type is determined by the space. 
    Game.ActionType requiredActionType;

    public override bool Can()
    {
        // Is the space not already friendly-flagged? 
        Space space = GetComponent<Space>();

        if (space.flag == player.faction) return false; // Note: Take Advantage of the Conditionals! 

        // Let's establish the cost of the Action. Assume political space for the time being
        ActionPoint ap = new ActionPoint();
        ap.actionTier = space.flag == Game.Faction.Neutral ? Game.ActionTier.Minor : Game.ActionTier.Major; 
        ap.actionType = requiredActionType;
        ap.actionPoints = space.conflictMarker ? 1 : space.flagCost; 

        return CanAfford(actionPointCost.Merge(ap)); 
    }
}
