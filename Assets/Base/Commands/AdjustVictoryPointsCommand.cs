using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustVictoryPointsCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is IScoreVP vpAction && vpAction.vp != 0)
            GameObject.FindObjectOfType<RecordsTrack>().VictoryPoints += (vpAction.faction == Game.Faction.Britain ? vpAction.vp : -vpAction.vp);
    }
}
