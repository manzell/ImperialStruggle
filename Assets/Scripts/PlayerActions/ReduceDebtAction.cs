using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceDebtAction : PlayerAction
{
    Faction faction; 
    public int debtAdjustment;


    protected override void Do()
    {
        commands.Push(new AdjustDebtCommand(actingPlayer.faction, debtAdjustment));
    }
}
