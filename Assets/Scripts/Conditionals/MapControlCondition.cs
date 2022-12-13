using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class MapControlCondition : Conditional
    {
        [SerializeField] Map map;
        public override bool Test(GameAction context)
        {
            if (context is PlayerAction playerAction)
                return map.controllingFaction == playerAction.player.faction;
            return false;
        }
    }
}