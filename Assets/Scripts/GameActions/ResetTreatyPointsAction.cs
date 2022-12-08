using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ResetTreatyPointsAction : GameAction
{
    [SerializeField] int treatyPointsCap = 4;

    protected override void Do()
    {
        foreach (Faction faction in Player.players.Select(p => p.faction))
            commands.Add(new AdjustTreatyPointsCommand(faction, Mathf.Min(0, treatyPointsCap - RecordsTrack.treatyPoints[faction]))); 
    }
}
