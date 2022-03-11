using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustTreatyPoints : GameAction
{
    public int adjustAmount;
    int previousTP;

    public AdjustTreatyPoints(Game.Faction faction, int adjustAmount)
    {
        actingFaction = faction;
        this.adjustAmount = adjustAmount;
        previousTP = RecordsTrack.treatyPoints[faction];
        Do(actingFaction); 
    }

    public override void Do(Game.Faction faction)
    {
        Debug.Log($"{faction} treaty points {(adjustAmount > 0 ? "increased" : "decreased")} by {Mathf.Abs(adjustAmount)}");
        RecordsTrack.treatyPoints[faction] += adjustAmount;
    }

    public override void Undo() => RecordsTrack.treatyPoints[actingFaction] = previousTP; 
}