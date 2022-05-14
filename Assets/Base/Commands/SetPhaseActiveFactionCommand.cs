using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPhaseActiveFactionCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is ITargetType<Game.Faction> factionTarget && action is ITargetType<ActionRound> arTarget)
        {
            Debug.Log($"Setting initiative to {factionTarget.target} for {arTarget.target.name}");
            arTarget.target.actingFaction = factionTarget.target; 
        }
        if(action is PlayerAction playerAction && action is ITargetType<ActionRound> _arTarget)
        {
            // Set all PlayerActions within the targetPhase and 
            foreach(PlayerAction pAction in _arTarget.target.GetComponentsInChildren<PlayerAction>())
            {
                Debug.Log($"Setting {pAction.actionName} player to {playerAction.player} on {_arTarget.target.name}");
                pAction.player = playerAction.player; 
            }
        }
    }
}