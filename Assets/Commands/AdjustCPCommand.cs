using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCPCommand : Command
{
    [SerializeField] Calculation<int> adjustAmount;
    int previousCP;
    Player previousPlayer;
    public Game.Faction targetFaction;

    public override void Do(Action a)
    {
        Debug.Log($"{targetFaction} Conquest Points {(adjustAmount.value > 0 ? "increased" : "decreased")} by {Mathf.Abs(adjustAmount.value)}");
        previousPlayer = Player.players[targetFaction];
        Player.players[targetFaction].CP += adjustAmount.value;
    }

    public override void Undo() => previousPlayer.CP = previousCP;
}
