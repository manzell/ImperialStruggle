using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class TerritoryBonus : ScoringBonus
{
    public List<Space> spaces;

    public override Faction scoringFaction {
        get 
        {
            int britainScore = spaces.Count(space => space.Flag == Game.Britain);
            int franceScore = spaces.Count(space => space.Flag == Game.France);

            if (britainScore > franceScore)
                return Game.Britain;
            else if(franceScore > britainScore)
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
