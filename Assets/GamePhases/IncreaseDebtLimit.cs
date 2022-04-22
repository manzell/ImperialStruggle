using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDebtLimit : Command
{
    Game.Faction targetFaction;
    int adjustAmount;

    public override void Do(Action action) => GameObject.FindObjectOfType<RecordsTrack>().debtLimit[targetFaction] += adjustAmount;
    public override void Undo() => GameObject.FindObjectOfType<RecordsTrack>().debtLimit[targetFaction] -= adjustAmount;
}
