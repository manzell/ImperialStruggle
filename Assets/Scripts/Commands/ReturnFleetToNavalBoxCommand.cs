using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class ReturnFleetToNavalBoxCommand : Command
    {
        NavalSpace navalSpace; 

        public ReturnFleetToNavalBoxCommand(NavalSpace navalSpace) => this.navalSpace = navalSpace;

        public override void Do(GameAction action)
        {
            navalSpace.Squadron.space = null; // TODO - Make this the Naval Box
            navalSpace.SetFlag(null); 
        }
    }
}
