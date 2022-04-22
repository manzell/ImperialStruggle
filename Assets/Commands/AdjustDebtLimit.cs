using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AdjustDebtLimit : Command
{
    public static UnityEvent<Game.Faction, int> adjustDebtLimitEvent = new UnityEvent<Game.Faction, int>();
    public RecordsTrack recordsTrack;
    public Game.Faction targetFaction; 
    public int adjustAmt;
    int previousDebtLimit;

    public override void Do(Action action)
    {
        previousDebtLimit = recordsTrack.debtLimit[targetFaction];
        recordsTrack.debtLimit[targetFaction] += adjustAmt;
        Debug.Log($"{targetFaction} debt limit {(adjustAmt > 0 ? "increased" : "decreased")} by {Mathf.Abs(adjustAmt)}");

        if (recordsTrack.availableDebt[targetFaction] < 0)
            recordsTrack.currentDebt[targetFaction] = recordsTrack.debtLimit[targetFaction]; 
    }
}
