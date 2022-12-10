using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public static class RecordsTrack
{
    public static System.Action adjustDebtLimitEvent, adjustDebtEvent, adjustVPEvent, adjustTPEvent;

    public static int VictoryPoints; public static Dictionary<Faction, int> debtLimit = new ();
    public static Dictionary<Faction, int> currentDebt = new ();
    public static Dictionary<Faction, int> treatyPoints = new ();
    public static Dictionary<Faction, int> availableDebt => debtLimit.ToDictionary(kvp => kvp.Key, kvp => debtLimit[kvp.Key] - currentDebt[kvp.Key]);



}
