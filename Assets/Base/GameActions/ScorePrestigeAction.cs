using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class ScorePrestigeAction : GameAction, IScoreVP
{
    [SerializeField] int prestigeVPvalue = 2;
    [SerializeField] List<Space> usaSpaces; 

    public int vp => prestigeVPvalue;
    public Game.Faction faction
    {
        get
        {
            List<Space> spaces = FindObjectsOfType<Space>().ToList(); 
            List<Space> prestigeSpaces = spaces.Where(space => space.prestige == true).ToList();

            if (spaces.Any(space => space.flag == Game.Faction.USA))
                prestigeSpaces.AddRange(usaSpaces); 

            int britainScore = prestigeSpaces.Where(space => space.flag == Game.Faction.Britain).Count();
            int franceScore = prestigeSpaces.Where(space => space.flag == Game.Faction.France).Count();

            if (britainScore > franceScore)
                return Game.Faction.Britain;
            else if (franceScore > britainScore)
                return Game.Faction.France;
            return Game.Faction.Neutral;
        }
    }
}
