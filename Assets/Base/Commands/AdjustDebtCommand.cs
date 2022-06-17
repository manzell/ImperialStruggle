using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustDebtCommand : Command
{
    public Game.Faction faction; 
    public int amount; 

    public override void Do(BaseAction action)
    {
        if (amount != 0 && faction != Game.Faction.Neutral)
        {
            RecordsTrack.currentDebt[faction] += amount;
            RecordsTrack.adjustDebtEvent.Invoke(); 
            // Check to see if Debt > Debt Limit
        }
    }
}
