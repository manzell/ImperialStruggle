using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public static class RecordsTrack
{
    public static Dictionary<Game.Faction, int> debtLimit = new Dictionary<Game.Faction, int>();
    public static Dictionary<Game.Faction, int> currentDebt = new Dictionary<Game.Faction, int>();
    public static Dictionary<Game.Faction, int> treatyPoints = new Dictionary<Game.Faction, int>();
    public static int VictoryPoints;

    public static UnityEvent adjustDebtLimitEvent = new UnityEvent(),
        adjustDebtEvent = new UnityEvent(),
        adjustVPEvent = new UnityEvent(),
        adjustTPEvent = new UnityEvent();

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
