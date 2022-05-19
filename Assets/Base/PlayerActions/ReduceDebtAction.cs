using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceDebtAction : PlayerAction, IAdjustDebt
{
    public int debtAdjustment; 
    public Game.Faction faction => player.faction;
    public int debt => -debtAdjustment;

}
