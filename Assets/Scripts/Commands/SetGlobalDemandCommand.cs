using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System;
using Sirenix.Utilities;

namespace ImperialStruggle
{
    public class SetGlobalDemandCommand : Command
    {
        public static UnityEvent<PeaceTurn> setGlobalDemandEvent = new UnityEvent<PeaceTurn>();

        IEnumerable<Resource> resources;
        public SetGlobalDemandCommand(IEnumerable<Resource> resources) => this.resources = resources;

        public override void Do(GameAction action)
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
            {
                peaceTurn.globalDemandResources = new(resources);
                setGlobalDemandEvent.Invoke(peaceTurn);

                resources.ForEach(resource => Debug.Log($"{resource.Name} is in Global Demand!"));
            }
        }
    }
}