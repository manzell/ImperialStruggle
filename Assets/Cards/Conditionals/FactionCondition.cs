using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionCondition : Conditional<Game.Faction>
{
    public Game.Faction faction;

    public override bool Test(Game.Faction faction) => this.faction == Game.Faction.Neutral ? true : faction == this.faction; 
}
