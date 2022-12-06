using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System;
using Sirenix.Utilities;

public class SetGlobalDemandCommand : Command
{
    public static UnityEvent<PeaceTurn> setGlobalDemandEvent = new UnityEvent<PeaceTurn>();

    HashSet<Resource> resources;

    public SetGlobalDemandCommand(IEnumerable<Resource> resources) => this.resources = new HashSet<Resource>(resources); 

    public override void Do(GameAction action)
    {
        if(Phase.CurrentPhase is PeaceTurn peaceTurn)
        {
            peaceTurn.globalDemandResources = resources; 
            setGlobalDemandEvent.Invoke(peaceTurn);
        }
    }
}
