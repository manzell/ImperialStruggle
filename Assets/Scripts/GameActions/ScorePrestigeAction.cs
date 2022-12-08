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
        HashSet<ISpace> prestigeSpaces = new (Game.Spaces.OfType<PrestigeSpace>().Where(space => space.Prestigious));

        if (usaSpaces.Any(space => space.Flag == Game.USA))
            prestigeSpaces.UnionWith(usaSpaces);

        int britainScore = prestigeSpaces.Count(space => space.Flag == Game.Britain);
        int franceScore = prestigeSpaces.Count(space => space.Flag == Game.France);

        if (britainScore > franceScore)
            adjustVPCommand.faction = Game.Britain;
        else if (franceScore > britainScore)
            adjustVPCommand.faction = Game.France;

        commands.Add(adjustVPCommand); 
    }
}
