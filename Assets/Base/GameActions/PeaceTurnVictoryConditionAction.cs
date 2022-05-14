using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class PeaceTurnVictoryConditionAction : GameAction
{
    protected override void Do(UnityAction callback)
    {
        Dictionary<Game.Resource, Game.Faction> demandWinners = Phase.currentPhase.GetComponentInChildren<ScoreGlobalDemandAction>().globalDemandWinners; 
        Dictionary<Map, Game.Faction> mapWinners = Phase.currentPhase.GetComponentInChildren<ScoreMapAction>().mapWinners; 

        foreach(Game.Faction faction in Player.players.Keys)
        {
            if(demandWinners.Count > 0 && mapWinners.Count > 0 && demandWinners.All(kvp => kvp.Value == faction) && mapWinners.All(kvp => kvp.Value == faction))
            {
                Debug.Log($"GAME WINNER EVENT - {faction} Wins!"); 
            }
        }

        base.Do(callback);
    }
}
