using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustActionPointsCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is IAdjustActionPoints apAction)
        {
            foreach(ActionPoint ap in apAction.actionPoints)
            {
                apAction.player.actionPoints.Add(ap);
                Debug.Log($"{apAction.player} {ap.actionPoints}-{ap.actionTier}-{ap.actionType}");
            }
        }
    }
}