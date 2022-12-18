using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace ImperialStruggle
{
    public class ResetTreatyPointsAction : GameAction
    {
        [SerializeField] int treatyPointsCap = 4;

        protected override Task Do()
        {
            foreach (Faction faction in Player.Players.Select(p => p.Faction))
                Commands.Push(new AdjustTreatyPointsCommand(faction, Mathf.Min(0, treatyPointsCap - RecordsTrack.treatyPoints[faction])));

            return Task.CompletedTask; 
        }
    }
}