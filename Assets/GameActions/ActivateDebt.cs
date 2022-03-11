using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDebt : GameAction
{
    public int adjustAmt; 

    public ActivateDebt(Game.Faction faction, int amount)
    {
        adjustAmt = amount;
        Do(faction); 
    }


    public override void Do(Game.Faction faction)
    {
        Debug.Log($"{faction} takes {adjustAmt} Debt");
        RecordsTrack.currentDebt[faction]++; 
    }
}
