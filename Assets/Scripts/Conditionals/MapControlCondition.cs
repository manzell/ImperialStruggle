using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class MapControlCondition : Conditional<Faction>
    {
        [SerializeField] Map map;
        protected override bool Test(Faction faction) => map.controllingFaction == faction;
    }
}