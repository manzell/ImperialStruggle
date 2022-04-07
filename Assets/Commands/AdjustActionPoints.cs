using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AdjustActionPoints : Command
{
    public static UnityEvent<AdjustActionPoints> adjustActionPointsEvent = new UnityEvent<AdjustActionPoints>();

    Dictionary<(Game.ActionType, Game.ActionTier), int> actionPointAdjustment = new Dictionary<(Game.ActionType, Game.ActionTier), int>(),
        previousActionPoints = new Dictionary<(Game.ActionType, Game.ActionTier), int>(); 

    public AdjustActionPoints(Game.Faction faction, Dictionary<(Game.ActionType, Game.ActionTier), int> actionPoints)
    {
        actionPointAdjustment = actionPoints; 
        Do(faction);
    }

    public override void Do(Game.Faction faction)
    {
        Player player = Player.players[faction];
        previousActionPoints = player.actionPoints;
        actingFaction = faction;

        foreach (KeyValuePair<(Game.ActionType type, Game.ActionTier tier), int> kvp in actionPointAdjustment)
        {
            Debug.Log($"{faction} {(kvp.Value > 0 ? "+" : "")}{kvp.Value} {kvp.Key.tier} {kvp.Key.type} Points");

            if (player.actionPoints.TryGetValue(kvp.Key, out int v))
                player.actionPoints[kvp.Key] += v;
            else
                player.actionPoints.Add(kvp.Key, kvp.Value); 
        }

        adjustActionPointsEvent.Invoke(this); 
    }

    public override void Undo()
    {
        Player.players[actingFaction].actionPoints = previousActionPoints;
    }
}
