using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDebtLimit : Command
{
    public Dictionary<Game.Faction, int> debtLimitIncrease;
    RecordsTrack recordsTrack;

    public void Do()
    {
        recordsTrack = GameObject.FindObjectOfType<RecordsTrack>();
        foreach (Game.Faction faction in debtLimitIncrease.Keys)
            recordsTrack.debtLimit[faction] += debtLimitIncrease[faction];
    }

    public override void Undo()
    {
        foreach (Game.Faction faction in debtLimitIncrease.Keys)
            recordsTrack.debtLimit[faction] -= debtLimitIncrease[faction];
    }
}
