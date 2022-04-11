using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustConquestPoints : Command
{
    public int adjustAmount;
    int previousCP;
    Player previousPlayer; 

    public AdjustConquestPoints(Game.Faction faction, int adjustAmount)
    {
        targetFaction = faction;
        this.adjustAmount = adjustAmount;
        previousCP = Player.players[faction].CP; 
        Do(targetFaction);
    }

    public override void Do(Game.Faction faction)
    {
        Debug.Log($"{faction} Conquest Points {(adjustAmount > 0 ? "increased" : "decreased")} by {Mathf.Abs(adjustAmount)}");
        previousPlayer = Player.players[faction];
        Player.players[faction].CP += adjustAmount;
    }

    public override void Undo() => previousPlayer.CP = previousCP;
}
