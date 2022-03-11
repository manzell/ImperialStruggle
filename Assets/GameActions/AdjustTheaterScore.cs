using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustTheaterScore : GameAction
{
    public int adjustAmount;
    int previousTS;
    Theater theater; 

    public AdjustTheaterScore(Game.Faction faction, Theater theater, int adjustAmount)
    {
        actingFaction = faction;
        this.adjustAmount = adjustAmount;
        this.theater = theater;
        previousTS = theater.theaterScore[faction]; 
        Do(actingFaction);
    }

    public override void Do(Game.Faction faction)
    {
        Debug.Log($"{faction} {(adjustAmount > 0 ? "gains" : "loses")} {adjustAmount} War Score in {theater}");
        theater.theaterScore[actingFaction] += adjustAmount;
    }

    public override void Undo() => theater.theaterScore[actingFaction] = previousTS;
}
