using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AdjustDebtCommand : Command
{
    public static UnityEvent<Game.Faction, int> adjustDebtEvent = new UnityEvent<Game.Faction, int>(); 
    public int adjustAmt; 
    int previousDebt;
    public RecordsTrack recordsTrack; 

    public override void Do(Game.Faction faction)
    {
        previousDebt = recordsTrack.currentDebt[targetFaction];
        recordsTrack.currentDebt[targetFaction] += adjustAmt;
        Debug.Log($"{targetFaction} debt {(adjustAmt > 0 ? "increased" : "decreased")} by {Mathf.Abs(adjustAmt)}");
    }

    public override void Undo() => recordsTrack.currentDebt[targetFaction] = previousDebt;
}
