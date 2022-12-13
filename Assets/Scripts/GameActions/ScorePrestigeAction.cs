using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace ImperialStruggle
{
    public class ScorePrestigeAction : GameAction
    {
        [SerializeField] int VPaward; 
        [SerializeField] List<SpaceData> usaSpaces;

        protected override void Do()
        {
            HashSet<ISpace> prestigeSpaces = new(Game.Spaces.OfType<PrestigeSpace>().Where(space => space.Prestigious));

            if (usaSpaces.Any(spaceData => Game.SpaceLookup[spaceData].Flag == Game.USA))
                prestigeSpaces.UnionWith(usaSpaces.Select(spaceData => Game.SpaceLookup[spaceData]));

            int britainScore = prestigeSpaces.Count(space => space.Flag == Game.Britain);
            int franceScore = prestigeSpaces.Count(space => space.Flag == Game.France);

            if (britainScore > franceScore)
                Commands.Push(new AdjustVPCommand(Game.Britain, VPaward)); 
            else if (franceScore > britainScore)
                Commands.Push(new AdjustVPCommand(Game.France, VPaward));

        }
    }
}