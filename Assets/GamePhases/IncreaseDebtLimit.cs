using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDebtLimit : GameAction
{
    public Dictionary<Game.Faction, int> debtLimitIncrease;

    public void Do()
    {
        foreach (Game.Faction faction in debtLimitIncrease.Keys)
            RecordsTrack.debtLimit[faction] += debtLimitIncrease[faction];
    }

    public override void Undo()
    {
        foreach (Game.Faction faction in debtLimitIncrease.Keys)
            RecordsTrack.debtLimit[faction] -= debtLimitIncrease[faction];
    }
}
