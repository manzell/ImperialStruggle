using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map : ScriptableObject, ISelectable
{
    public AwardTile awardTile;
    public IEnumerable<Space> spaces => Game.Spaces.Where(space => space.map == this);

    public Dictionary<Faction, int> mapScore => spaces.GroupBy(space => space.Flag).ToDictionary(group => group.Key, group => group.Count()); 

    public Faction controllingFaction
    {
        get
        {
            int maxGameScore = mapScore.Max(kvp => kvp.Value); 
            int winningMargin = maxGameScore - mapScore.Values.OrderByDescending(val => val).ElementAt(1); 

            List<Faction> winningFactions = new List<Faction>(); 

            foreach(Player player in Player.players)
                if(mapScore[player.faction] == maxGameScore && winningMargin >= awardTile.RequiredMargin) // Need to move the margin logic out to the ScoreMapAction?
                    winningFactions.Add(player.faction);

            if(winningFactions.Count == 1)
                return winningFactions[0];
            else 
                return null;
        }
    }
}
