using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class TakeDebt : Command
{
    public static UnityEvent<TakeDebt> takeDebtEvent = new UnityEvent<TakeDebt>();
    int amount;
    RecordsTrack recordsTrack; 

    public void Do(TakeDebt td)
    {
    }

    public void Undo(TakeDebt td) 
    {
        recordsTrack.currentDebt[td.targetFaction] = Mathf.Clamp(recordsTrack.currentDebt[td.targetFaction] -= td.amount, 0, 99);
    }    
}