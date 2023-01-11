using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class SpaceCalculation : Calculation<HashSet<Space>>
    {
        [SerializeField] List<Map> eligibleMaps = new();
        [SerializeField] List<Region> eligibleRegions = new ();

        protected override HashSet<Space> Calc(Player player) => new(Game.Spaces.Where(space => (eligibleMaps.Count == 0 || eligibleMaps.Contains(space.map)) && 
            (eligibleRegions.Count == 0 || eligibleRegions.Contains(space.Data.Region))
        )); 
    }
}