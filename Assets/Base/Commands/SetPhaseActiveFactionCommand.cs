using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPhaseActiveFactionCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is ITargetType<Player> player && action is ITargetType<ActionRound> arTarget)
        {
            //Debug.Log($"Setting initiative to {faction.target} for {arTarget.target.name}");
            arTarget.target.actingPlayer = player.target; 
            
            foreach (PlayerAction pAction in arTarget.target.GetComponentsInChildren<PlayerAction>())
            {
                //Debug.Log($"Setting {pAction.actionName} player to {faction.target} on {arTarget.target.name}");
                pAction.player = player.target;
            }
        }
    }
}