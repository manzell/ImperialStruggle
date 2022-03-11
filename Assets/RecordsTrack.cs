using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RecordsTrack
{
    public static Dictionary<Game.Faction, int> debtLimit;
    public static Dictionary<Game.Faction, int> currentDebt;
    public static Dictionary<Game.Faction, int> treatyPoints; 

    public static Dictionary<Game.Faction, int> availableDebt
    {
        get
        {
            Dictionary<Game.Faction, int> retVal = new Dictionary<Game.Faction, int>(); 

            foreach(Game.Faction faction in debtLimit.Keys)
                retVal.Add(faction, debtLimit[faction] - currentDebt[faction]); 

            return retVal; 
        }
    }
}
