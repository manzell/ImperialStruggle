using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AdjustVictoryPoints : Command
{
    public int adjustAmount;
    public static UnityEvent<AdjustVictoryPoints> adjustVPEvent = new UnityEvent<AdjustVictoryPoints>();

    public AdjustVictoryPoints(Game.Faction faction, int adjustAmount)
    {
        actingFaction = faction;
        this.adjustAmount = faction == Game.Faction.England ? -adjustAmount : adjustAmount;
        
        Do(actingFaction);
    }

    public override void Do(Game.Faction faction)
    {
        if (faction == Game.Faction.England || faction == Game.Faction.France)
        {
            Debug.Log($"{faction} victory points increased by {Mathf.Abs(adjustAmount)}");
            VictoryPointTrack.VP += adjustAmount;

            adjustVPEvent.Invoke(this);
        }
    }

    public override void Undo() => VictoryPointTrack.VP -= adjustAmount;
}