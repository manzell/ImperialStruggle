using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustVPCommand : Command
{
    [SerializeField] Player player;
    [SerializeField] int amount; 

    public override void Do(BaseAction action)
    {
        if(action is IAdjustVP vpAction && vpAction.vp != 0)
        {
            RecordsTrack.VictoryPoints += (vpAction.faction == Game.Faction.Britain ? vpAction.vp : -vpAction.vp);
            RecordsTrack.adjustVPEvent.Invoke(); 
        }
        else if(player != null && amount != 0)
        {
            RecordsTrack.VictoryPoints += (player.faction == Game.Faction.Britain ? amount : -amount);
            RecordsTrack.adjustVPEvent.Invoke();
        }
    }
}
