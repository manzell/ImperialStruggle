using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustDebtLimitEvent : CardEvent
{
    [SerializeField] int amount; 
    public override void Event()
    {
        Game.Faction opposingFaction = faction == Game.Faction.England ? Game.Faction.France : Game.Faction.England;

        Phase.currentPhase.gameActions.Add(new AdjustDebtLimit(faction, amount)); 
    }
}
