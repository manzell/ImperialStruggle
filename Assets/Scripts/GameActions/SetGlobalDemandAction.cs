using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetGlobalDemandAction : GameAction
{
    int numGlobalDemandTiles = 3; 
    protected override void Do()
    {
        Game game = FindObjectOfType<Game>();
        commands.Push(new SetGlobalDemandCommand(game.Resources.OrderBy(resource => Random.value).Take(numGlobalDemandTiles)));
    }
}
