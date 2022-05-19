using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AdjustActionPointsCommand : Command
{
    public static UnityEvent adjustAPEvent = new UnityEvent();

    public override void Do(BaseAction action)
    {
        if(action is ITargetType<ActionPoints> APAction &&
            action is ITargetType<Player> playerAction)
        {
            foreach (ActionPoint ap in APAction.target)
            {
                adjustAPEvent.Invoke(); // the actual AP is passed in - and can be modified
                playerAction.target.actionPoints.Add(ap);
            }
        }

        if(action is IAdjustAP apAction)
        {
            foreach (ActionPoint ap in apAction.actionPoints)
            {
                adjustAPEvent.Invoke(); // the actual AP is passed in - and can be modified
                apAction.player.actionPoints.Add(ap);
            }
        }
    }
}