using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class ScoreMapPhase : MonoBehaviour
{
    public static UnityEvent<Map, Dictionary<Game.Faction, int>> mapScoreEvent = new UnityEvent<Map,Dictionary<Game.Faction, int>>();

    public void Do(Phase phase, UnityAction callback)
    {
        foreach(Map map in FindObjectsOfType<Map>())
        {
            Game.Faction winningFaction = Game.Faction.Neutral;
            Dictionary<Game.Faction, int> mapScore = new Dictionary<Game.Faction, int>() { 
                { Game.Faction.Britain, map.spaces.Where(space => space.flag == Game.Faction.Britain).Count()}, 
                { Game.Faction.France, map.spaces.Where(space => space.flag == Game.Faction.France).Count()}
            };          

            if (mapScore[Game.Faction.Britain] > mapScore[Game.Faction.France])
                winningFaction = Game.Faction.Britain;
            else if (mapScore[Game.Faction.France] > mapScore[Game.Faction.Britain])
                winningFaction = Game.Faction.France; 

            //if(winningFaction != Game.Faction.Neutral && map.awardTile.GetComponents<Conditional>().All(condition => condition.Test(map)))
            //{
            //    foreach (Command command in map.awardTile.GetComponents<Command>())
            //        command.Do();
            //}

            mapScoreEvent.Invoke(map, mapScore); 
        }

        callback.Invoke(); 
    }
}
