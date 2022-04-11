using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AdjustDebtLimit : Command
{
    public static UnityEvent<Game.Faction, int> adjustDebtLimitEvent = new UnityEvent<Game.Faction, int>();
    public RecordsTrack recordsTrack;
    public int adjustAmt;
    int previousDebtLimit;

    public AdjustDebtLimit(Game.Faction faction, int amount)
    {
        recordsTrack = GameObject.FindObjectOfType<RecordsTrack>();
        targetFaction = faction;
        adjustAmt = amount;
        previousDebtLimit = recordsTrack.currentDebt[faction];
        Do(faction);
    }

    public override void Do(Game.Faction faction)
    {
        previousDebtLimit = recordsTrack.debtLimit[targetFaction];
        recordsTrack.debtLimit[targetFaction] += adjustAmt;
        Debug.Log($"{targetFaction} debt limit {(adjustAmt > 0 ? "increased" : "decreased")} by {Mathf.Abs(adjustAmt)}");

        if (recordsTrack.availableDebt[targetFaction] < 0)
            recordsTrack.currentDebt[targetFaction] = recordsTrack.debtLimit[targetFaction]; 
    }
}
