using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

public static class Extensions
{
    public static Faction Opposition(this Faction faction)
    {
        if (faction == Game.Britain) return Game.France;
        else if (faction == Game.France) return Game.Britain;
        else return null; 
    }
}
