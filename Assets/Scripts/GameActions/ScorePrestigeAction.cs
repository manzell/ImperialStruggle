using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class ScorePrestigeAction : GameAction
{
    [SerializeField] List<Space> usaSpaces;
    [SerializeField] AdjustVPCommand adjustVPCommand; 

    protected override void Do()
    {
        HashSet<Space> prestigeSpaces = new HashSet<Space>(Game.Spaces.Keys.Where(space => space.prestige == true));

        if (usaSpaces.Any(space => space.flag == Game.USA))
            prestigeSpaces.UnionWith(usaSpaces);

        int britainScore = prestigeSpaces.Count(space => space.flag == Game.Britain);
        int franceScore = prestigeSpaces.Count(space => space.flag == Game.France);

        if (britainScore > franceScore)
            adjustVPCommand.faction = Game.Britain;
        else if (franceScore > britainScore)
            adjustVPCommand.faction = Game.France;

        commands.Push(adjustVPCommand); 
    }
}
