using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AdjustDebtLimitAction : GameAction
{
    [SerializeField] List<Faction> factions;
    public int amount;

    protected override void Do()
    {
        foreach(Faction faction in factions)
        {
            AdjustDebtCommand command = new AdjustDebtCommand(faction, amount); 
            commands.Push(command);
        }
    }
}