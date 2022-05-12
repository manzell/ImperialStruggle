using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustTreatyPointsCommand : Command
{
    public override void Do(BaseAction action)
    {
        if (action is IAdjustTP tpAction && tpAction.tp != 0)
            GameObject.FindObjectOfType<RecordsTrack>().treatyPoints[tpAction.faction] += tpAction.tp; 
    }
}
