using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustDebt : GameAction
{
    public int adjustAmt; 
    int previousDebt; 

    public AdjustDebt(Game.Faction faction, int amount)
    {
        actingFaction = faction;
        adjustAmt = amount; 
        previousDebt = RecordsTrack.currentDebt[faction];
        Do(actingFaction);
    }

    public override void Do(Game.Faction faction)
    {
        previousDebt = RecordsTrack.currentDebt[faction];
        RecordsTrack.currentDebt[faction] += adjustAmt;
        Debug.Log($"{faction} debt {(adjustAmt > 0 ? "increased" : "decreased")} by {Mathf.Abs(adjustAmt)}");
    }

    public override void Undo() => RecordsTrack.currentDebt[actingFaction] = previousDebt;
}
