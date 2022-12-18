using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class SetGlobalDemandAction : GameAction
    {
        [SerializeField] int numGlobalDemandTiles = 3;

        protected override Task Do()
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
                Commands.Push(new SetGlobalDemandCommand(Game.GlobalDemandTrack.GlobalDemandAwards.Keys
                    .OrderBy(res => Random.value).Select(gdk => gdk.Resource).Take(numGlobalDemandTiles)));

            return Task.CompletedTask; 
        }
    }
}