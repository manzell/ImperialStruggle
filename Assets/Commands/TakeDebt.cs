using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class TakeDebt : Command
{
    public static UnityEvent<TakeDebt> takeDebtEvent = new UnityEvent<TakeDebt>();
    int amount;
    RecordsTrack recordsTrack;

    public override void Do(BaseAction action)
    {
    }

    public void Undo(TakeDebt td) 
    {
        //recordsTrack.currentDebt[actingFaction] = Mathf.Clamp(recordsTrack.currentDebt[actingFaction] -= td.amount, 0, 99);
    }    
}