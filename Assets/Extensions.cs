using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Game.Faction Opposition(this Game.Faction faction)
    {
        if (faction == Game.Faction.Britain) return Game.Faction.France;
        else if (faction == Game.Faction.France) return Game.Faction.Britain;
        else return Game.Faction.Neutral; 
    }

}
