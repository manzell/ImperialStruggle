using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustVictoryPointsCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is IAdjustVP vpAction && vpAction.vp != 0)
        {
            RecordsTrack.VictoryPoints += (vpAction.faction == Game.Faction.Britain ? vpAction.vp : -vpAction.vp);
            RecordsTrack.adjustVPEvent.Invoke(); 
        }
    }
}
