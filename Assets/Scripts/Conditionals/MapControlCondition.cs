using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class MapControlCondition : Conditional
    {
        [SerializeField] Map map;
        public override bool Test(IPlayerAction context) => map.controllingFaction == context.Player.Faction;
    }
}