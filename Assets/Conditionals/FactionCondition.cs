using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionCondition : Conditional
{
    public Game.Faction targetFaction;

    public override bool Test(Object player)
    {
        if (player is Player)
        {
            Game.Faction faction = (player as Player).faction;
            return faction == Game.Faction.Neutral || faction == targetFaction;
        }

        return true;
    }
}
