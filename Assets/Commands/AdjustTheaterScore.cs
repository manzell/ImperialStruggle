using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustTheaterScore : Command
{
    [SerializeField] Theater theater;
    [SerializeField] Game.Faction targetFaction;
    [SerializeField] int adjustAmount;
    int previousTS;

    public override void Do(BaseAction action)
    {
        Debug.Log($"{targetFaction} {(adjustAmount > 0 ? "gains" : "loses")} {adjustAmount} War Score in {theater}");
        theater.theaterScore[targetFaction] += adjustAmount;
    }

    public override void Undo() => theater.theaterScore[targetFaction] = previousTS;
}
