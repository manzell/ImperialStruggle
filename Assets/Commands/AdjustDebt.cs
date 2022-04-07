using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AdjustDebt : Command
{
    public static UnityEvent<Game.Faction, int> adjustDebtEvent = new UnityEvent<Game.Faction, int>(); 
    public int adjustAmt; 
    int previousDebt;
    public RecordsTrack recordsTrack; 

    public AdjustDebt(Game.Faction faction, int amount)
    {
        recordsTrack = GameObject.FindObjectOfType<RecordsTrack>();
        actingFaction = faction;
        adjustAmt = amount;
        previousDebt = recordsTrack.currentDebt[faction];
        Do(faction);
    }

    public override void Do(Game.Faction faction)
    {
        previousDebt = recordsTrack.currentDebt[actingFaction];
        recordsTrack.currentDebt[actingFaction] += adjustAmt;
        Debug.Log($"{actingFaction} debt {(adjustAmt > 0 ? "increased" : "decreased")} by {Mathf.Abs(adjustAmt)}");
    }

    public override void Undo() => recordsTrack.currentDebt[actingFaction] = previousDebt;
}
