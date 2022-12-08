using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetGlobalDemandAction : GameAction
{
    int numGlobalDemandTiles = 3;

    protected override void Do()
    {
        if(Phase.CurrentPhase is PeaceTurn peaceTurn)
            commands.Add(new SetGlobalDemandCommand(Game.GlobalDemandTrack.Resources.OrderBy(r => Random.value).Take(numGlobalDemandTiles)));
    }
}
