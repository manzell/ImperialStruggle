using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace ImperialStruggle
{
    public class DeploySquadronCommand : Command
    {
        Squadron squadron;
        NavalSpace navalSpace; 
        public DeploySquadronCommand(Squadron squadron, NavalSpace space)
        {
            this.navalSpace = space;
            this.squadron = squadron;
        }

        public override void Do(GameAction action)
        {
            navalSpace.Squadron = squadron;
            squadron.space = navalSpace; 
        }
    }
}