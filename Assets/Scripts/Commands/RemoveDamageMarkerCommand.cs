using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class RemoveDamageMarkerCommand : Command
    {
        Fort fort; 
        public RemoveDamageMarkerCommand(Fort fort) => this.fort = fort; 

        public override void Do(GameAction action)
        {
            fort.damaged = false; 
        }
    }
}
