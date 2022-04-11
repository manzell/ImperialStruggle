using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AdjustAPCommand : Command
{
    public static UnityEvent<AdjustAPCommand> adjustActionPointsEvent = new UnityEvent<AdjustAPCommand>();
        
    public List<ActionPoint> actionPoints = new List<ActionPoint>(),
        previousActionPoints = new List<ActionPoint>();

    public override void Do(Game.Faction faction)
    {        
        Player player = Player.players[targetFaction];
        previousActionPoints = player.actionPoints;
        
        foreach(ActionPoint actionPoint in actionPoints)
            Game.Log($"{targetFaction} {(actionPoint.actionPoints > 0 ? "+" : "")}{actionPoint.actionPoints} {actionPoint.actionTier} {actionPoint.actionType} Points");

        player.actionPoints.AddRange(actionPoints);
        base.Do(faction);

        adjustActionPointsEvent.Invoke(this); 
    }

    public override void Undo()
    {
        foreach(ActionPoint actionPoint in actionPoints)
            Player.players[targetFaction].actionPoints.Remove(actionPoint);
    }
}
