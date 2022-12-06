using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Faction Opposition(this Faction faction)
    {
        if (faction == Game.Britain) return Game.France;
        else if (faction == Game.France) return Game.Britain;
        else return null; 
    }

}
