using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryBonus : ScoringBonus
{
    public List<Space> spaces;

    public override Faction scoringFaction {
        get 
        {
            Dictionary<Faction, int> spaceScore = new Dictionary<Faction, int>();
            spaceScore.Add(Game.Britain, 0);
            spaceScore.Add(Game.France, 0); 

            foreach(Space space in spaces)
            {
                if(spaceScore.ContainsKey(space.flag))
                    spaceScore[space.flag]++;
            }

            if(spaceScore[Game.Britain] > spaceScore[Game.France])
                return Game.Britain;
            else if(spaceScore[Game.France] > spaceScore[Game.Britain])
                return Game.France;
            else
                return Game.Neutral; 
        }
    }

    int _bonusValue = 1;
    public override int bonusValue { 
        get => _bonusValue; 
    }
}
