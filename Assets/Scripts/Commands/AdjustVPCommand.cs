using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustVPCommand : Command
{
    public Faction faction;
    [field: SerializeField] public int amount { get; private set; }

    int previousVP; 

    public AdjustVPCommand(Faction faction, int amount)
    {
        this.faction = faction;
        this.amount = amount;
    }

    public override void Do(GameAction action)
    {
        if(amount != 0)
        {
            previousVP = RecordsTrack.VictoryPoints; 
            RecordsTrack.VictoryPoints += amount * (faction == Game.Britain ? 1 : -1);
            RecordsTrack.adjustVPEvent.Invoke(); 
        }
    }

    public override void Undo()
    {
        RecordsTrack.VictoryPoints = previousVP;
    }
}
