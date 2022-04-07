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
        actingFaction = faction;
        adjustAmt = amount;
        previousDebtLimit = recordsTrack.currentDebt[faction];
        Do(faction);
    }

    public override void Do(Game.Faction faction)
    {
        previousDebtLimit = recordsTrack.debtLimit[actingFaction];
        recordsTrack.debtLimit[actingFaction] += adjustAmt;
        Debug.Log($"{actingFaction} debt limit {(adjustAmt > 0 ? "increased" : "decreased")} by {Mathf.Abs(adjustAmt)}");

        if (recordsTrack.availableDebt[actingFaction] < 0)
            recordsTrack.currentDebt[actingFaction] = recordsTrack.debtLimit[actingFaction]; 
    }
}
