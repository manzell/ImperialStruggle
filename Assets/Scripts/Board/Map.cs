using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class Map : MonoBehaviour, ICriteria, ISelectable
{
    public AwardTile awardTile;
    public List<Space> spaces; 

    public Dictionary<Faction, int> mapScore
    {
        get
        {
            Dictionary<Faction, int>  retVal = new Dictionary<Faction, int> {
                { Game.Britain, 0 },
                { Game.France, 0 }
            }; 

            foreach(Space space in spaces)
                if(retVal.ContainsKey(space.flag))
                    retVal[space.flag]++; 

            return retVal; 
        }
    }

    public Faction controllingFaction
    {
        get
        {
            int maxGameScore = mapScore.Values.OrderByDescending(val => val).First();
            int winningMargin = maxGameScore - mapScore.Values.OrderByDescending(val => val).ElementAt(1); 

            List<Faction> winningFactions = new List<Faction>(); 

            foreach(Player player in Player.players)
                if(mapScore[player.faction] == maxGameScore && winningMargin >= awardTile.requiredMargin) // Need to move the margin logic out to the ScoreMapAction
                    winningFactions.Add(player.faction);

            if(winningFactions.Count == 1)
                return winningFactions[0];
            else 
                return null;
        }
    }
}
