using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class Map : MonoBehaviour, ICriteria, ISelectable
{
    public AwardTile awardTile;
    public List<Space> spaces; 

    public Dictionary<Game.Faction, int> mapScore
    {
        get
        {
            Dictionary<Game.Faction, int>  retVal = new Dictionary<Game.Faction, int> {
                { Game.Faction.Britain, 0 },
                { Game.Faction.France, 0 }
            }; 

            foreach(Space space in spaces)
                if(retVal.ContainsKey(space.flag))
                    retVal[space.flag]++; 

            return retVal; 
        }
    }

    public Game.Faction controllingFaction
    {
        get
        {
            int maxGameScore = mapScore.Values.OrderByDescending(val => val).First();
            int winningMargin = maxGameScore - mapScore.Values.OrderByDescending(val => val).ElementAt(1); 

            List<Game.Faction> winningFactions = new List<Game.Faction>(); 

            foreach(Game.Faction faction in Player.players.Keys)
                if(mapScore[faction] == maxGameScore && winningMargin >= awardTile.requiredMargin) // Need to move the margin logic out to the ScoreMapAction
                    winningFactions.Add(faction);

            if(winningFactions.Count == 1)
                return winningFactions[0];
            else 
                return Game.Faction.Neutral;
        }
    }
}
