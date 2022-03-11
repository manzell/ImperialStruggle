using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class ChargeActionPoints : GameAction
{ 
    public Dictionary<Game.ActionType, int> charge = new Dictionary<Game.ActionType, int>();

    public static UnityEvent<ChargeActionPoints> chargeActionPointsEvent  = new UnityEvent<ChargeActionPoints>();
    Player player; 
    Dictionary<Game.ActionType, int>
        prevMajorActionPts = new Dictionary<Game.ActionType, int>(),
        prevMinorActionPts = new Dictionary<Game.ActionType, int>();

    public ChargeActionPoints(Game.Faction faction, Dictionary<Game.ActionType, int> charge)
    {
        actingFaction = faction;
        this.charge = charge;

        Do(faction); 
    }

    public override void Do(Game.Faction faction)
    {
        Dictionary<Game.ActionType, int> chargeRemaining = new Dictionary<Game.ActionType, int>(charge);
        player = Player.players[faction]; 
        prevMajorActionPts = player.majorActionPoints;
        prevMinorActionPts = player.minorActionPoints;


        string log = $"Charging {actingFaction} ";
        foreach (KeyValuePair<Game.ActionType, int> kvp in charge)
            log += $"{kvp.Value} {kvp.Key} points ";

        Debug.Log(log);

        foreach (Game.ActionType actionType in charge.Keys)
        {
            // First we charge any Treaty Points made available 
            int tpToCharge = Mathf.Min(chargeRemaining[actionType], player.majorActionPoints.ContainsKey(Game.ActionType.Treaty) ? player.majorActionPoints[Game.ActionType.Treaty] : 0);
            if (tpToCharge > 0)
            {
                player.majorActionPoints[Game.ActionType.Treaty] -= tpToCharge;
                chargeRemaining[actionType] -= tpToCharge;
            }

            // Then we charge any Debt made available
            int debtToCharge = Mathf.Min(chargeRemaining[actionType], player.majorActionPoints.ContainsKey(Game.ActionType.Debt) ? player.majorActionPoints[Game.ActionType.Debt] : 0);
            if (debtToCharge > 0)
            {
                player.majorActionPoints[Game.ActionType.Debt] -= debtToCharge;
                chargeRemaining[actionType] -= debtToCharge;
            }

            // Then we charge minor action points 
            int minorToCharge = Mathf.Min(chargeRemaining[actionType], player.minorActionPoints.ContainsKey(actionType) ? player.minorActionPoints[actionType] : 0);
            if (minorToCharge > 0)
            {
                player.minorActionPoints[actionType] -= minorToCharge;
                chargeRemaining[actionType] -= minorToCharge;
            }

            // Lastly we charge major action points 
            if (chargeRemaining[actionType] > 0)
            {
                player.majorActionPoints[actionType] -= chargeRemaining[actionType];
                chargeRemaining[actionType] -= chargeRemaining[actionType];
            }
        }

        chargeActionPointsEvent.Invoke(this);
    }

    public override void Undo()
    {
        player.majorActionPoints = prevMajorActionPts;
        player.minorActionPoints = prevMinorActionPts; 
    }
}
