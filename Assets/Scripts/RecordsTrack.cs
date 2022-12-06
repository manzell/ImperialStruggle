using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public static class RecordsTrack
{
    public static Dictionary<Faction, int> debtLimit = new Dictionary<Faction, int>();
    public static Dictionary<Faction, int> currentDebt = new Dictionary<Faction, int>();
    public static Dictionary<Faction, int> treatyPoints = new Dictionary<Faction, int>();
    public static int VictoryPoints;

    public static UnityEvent adjustDebtLimitEvent = new UnityEvent(),
        adjustDebtEvent = new UnityEvent(),
        adjustVPEvent = new UnityEvent(),
        adjustTPEvent = new UnityEvent();

    public static Dictionary<Faction, int> availableDebt
    {
        get
        {
            Dictionary<Faction, int> retVal = new Dictionary<Faction, int>(); 

            foreach(Faction faction in debtLimit.Keys)
                retVal.Add(faction, debtLimit[faction] - currentDebt[faction]); 

            return retVal; 
        }
    }
}
