using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryBonus : ScoringBonus
{
    public List<Space> spaces;

    public override Game.Faction scoringFaction {
        get 
        {
            Dictionary<Game.Faction, int> spaceScore = new Dictionary<Game.Faction, int>();
            spaceScore.Add(Game.Faction.Britain, 0);
            spaceScore.Add(Game.Faction.France, 0); 

            foreach(Space space in spaces)
            {
                if(spaceScore.ContainsKey(space.flag))
                    spaceScore[space.flag]++;
            }

            if(spaceScore[Game.Faction.Britain] > spaceScore[Game.Faction.France])
                return Game.Faction.Britain;
            else if(spaceScore[Game.Faction.France] > spaceScore[Game.Faction.Britain])
                return Game.Faction.France;
            else
                return Game.Faction.Neutral; 
        }
    }

    int _bonusValue = 1;
    public override int bonusValue { 
        get => _bonusValue; 
    }
}
