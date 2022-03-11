using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTreatyPoint : GameAction
{
    public int adjustAmt;

    public ActivateTreatyPoint(Game.Faction faction, int amount)
    {
        adjustAmt = amount;
        Do(faction);
    }

    public override void Do(Game.Faction faction)
    {
        Debug.Log($"{faction} spends {adjustAmt} Treaty {(adjustAmt == 1 ? "Point" : "Points")}");
        RecordsTrack.treatyPoints[faction] -= adjustAmt; 
    }
}