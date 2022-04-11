using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustDebtEvent : CardEvent
{
    [SerializeField] int amount;
    [SerializeField] Game.Faction targetFaction; 

    public override void Event()
    {

    }
}
