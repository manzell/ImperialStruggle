using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AdjustActionPoints : GameAction
{
    public static UnityEvent<AdjustActionPoints> adjustActionPointsEvent = new UnityEvent<AdjustActionPoints>();

    Dictionary<Game.ActionType, int> prevMajorActionPoints, prevMinorActionPoints, majorActionPoints, minorActionPoints; 

    public AdjustActionPoints(Game.Faction faction, Dictionary<Game.ActionType, int> majorActionPoints, Dictionary<Game.ActionType, int> minorActionPoints)
    {
        this.majorActionPoints = majorActionPoints;
        this.minorActionPoints = minorActionPoints;
        Do(faction); 
    }

    public override void Do(Game.Faction faction)
    {
        this.actingFaction = faction;
        Player player = Player.players[faction];

        prevMajorActionPoints = player.majorActionPoints;
        prevMinorActionPoints = player.minorActionPoints;

        foreach (KeyValuePair<Game.ActionType, int> pair in majorActionPoints)
        {
            if (player.majorActionPoints.ContainsKey(pair.Key))
                player.majorActionPoints[pair.Key] += pair.Value;
            else
                player.majorActionPoints.Add(pair.Key, pair.Value);

            player.majorActionPoints[pair.Key] = Mathf.Max(pair.Value, 0);
        }

        foreach (KeyValuePair<Game.ActionType, int> pair in minorActionPoints)
        {
            if (player.minorActionPoints.ContainsKey(pair.Key))
                player.minorActionPoints[pair.Key] += pair.Value;
            else
                player.minorActionPoints.Add(pair.Key, pair.Value);

            player.minorActionPoints[pair.Key] = Mathf.Max(pair.Value, 0);
        }

        adjustActionPointsEvent.Invoke(this); 
    }

    public override void Undo()
    {
        Player.players[actingFaction].majorActionPoints = prevMajorActionPoints;
        Player.players[actingFaction].minorActionPoints = prevMinorActionPoints;
    }
}
