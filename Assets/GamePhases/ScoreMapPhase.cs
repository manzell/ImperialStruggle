using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class ScoreMapPhase : MonoBehaviour, IPhaseAction
{
    public static UnityEvent<Map, Dictionary<Game.Faction, int>> mapScoreEvent = new UnityEvent<Map,Dictionary<Game.Faction, int>>();

    public void Do(Phase phase, UnityAction callback)
    {
        foreach(Map map in FindObjectsOfType<Map>())
        {
            Dictionary<Game.Faction, int> mapScore = new Dictionary<Game.Faction, int>() { 
                { Game.Faction.England, 0}, 
                { Game.Faction.France, 0 } 
            };

            foreach(Space space in map.spaces)
            {
                if(mapScore.ContainsKey(space.flag))
                    mapScore[space.flag]++;
            }

            Game.Faction winningFaction = Game.Faction.Neutral; 

            if (mapScore[Game.Faction.England] - map.awardTile.marginRequired >= mapScore[Game.Faction.France])
                winningFaction = Game.Faction.England;
            else if(mapScore[Game.Faction.France] - map.awardTile.marginRequired >= mapScore[Game.Faction.England])
                winningFaction = Game.Faction.France;

            if(map.awardTile.victoryPoints + map.bonusVP > 0)
                phase.gameActions.Add(new AdjustVictoryPoints(winningFaction, map.awardTile.victoryPoints + map.bonusVP));

            if (map.awardTile.treatyPoints> 0)
                phase.gameActions.Add(new AdjustTreatyPoints(winningFaction, map.awardTile.treatyPoints));

            mapScoreEvent.Invoke(map, mapScore); 
        }

        callback.Invoke(); 
    }
}
