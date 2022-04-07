using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class TakeDebt : Command
{
    public static UnityEvent<TakeDebt> takeDebtEvent = new UnityEvent<TakeDebt>();
    int amount;
    RecordsTrack recordsTrack; 

    public TakeDebt(Game.Faction faction, int amount)
    {
        recordsTrack = GameObject.FindObjectOfType<RecordsTrack>();
        actingFaction = faction;
        this.amount = amount;
        
        Do(this); 
    }

    public void Do(TakeDebt td)
    {
        if(recordsTrack.availableDebt[td.actingFaction] > td.amount)
        {
            recordsTrack.currentDebt[td.actingFaction] += td.amount;
            Dictionary<(Game.ActionType, Game.ActionTier), int> actionPoints = Player.players[td.actingFaction].actionPoints;

            if (actionPoints.ContainsKey((Game.ActionType.Debt, Game.ActionTier.Major)))
                actionPoints[(Game.ActionType.Debt, Game.ActionTier.Major)] += td.amount;
            else
                actionPoints.Add((Game.ActionType.Debt, Game.ActionTier.Major), td.amount);

            Debug.Log($"{td.actingFaction} takes {amount} debt (limit: {recordsTrack.debtLimit[td.actingFaction]}"); 
            takeDebtEvent.Invoke(td); 
        }
    }

    public void Undo(TakeDebt td) 
    {
        recordsTrack.currentDebt[td.actingFaction] = Mathf.Clamp(recordsTrack.currentDebt[td.actingFaction] -= td.amount, 0, 99);
    }
    
}
