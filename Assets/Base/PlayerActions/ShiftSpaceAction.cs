using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Sirenix.OdinInspector; 

public class ShiftSpaceAction : PlayerAction
{
    public ActionPoints actionPointBonusCost; // This is essentially a Modifier cost, since our actual cost and cost type is determined by the space. 
    Game.ActionType requiredActionType;

    [Button]
    public override bool Can()
    {
        // Is the space not already friendly-flagged? 
        Space space = GetComponent<Space>();

        if(Phase.currentPhase.TryGetComponent(out ActionRound actionRound))
            player = Player.players[actionRound.actingFaction]; 

        if(player != null)
        {
            if (space.flag == player.faction) return false; // Note: Take Advantage of the Conditionals! 

            // Let's establish the cost of the Action. 
            ActionPoint ap = new ActionPoint();
            ap.actionTier = space.flag == Game.Faction.Neutral ? Game.ActionTier.Minor : Game.ActionTier.Major;
            ap.actionType = requiredActionType;
            ap.actionPoints = space.conflictMarker ? 1 : space.flagCost;

            //ActionPoints needs better summation abilities 

            Debug.Log($"Shift Cost for {player.faction}: {ap.actionTier}-{ap.actionType}-{ap.actionPoints}");

            return base.Can();
        }

        return false; 
    }
}
