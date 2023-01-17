using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class DamageFortCommand : Command
    {
        Fort fort;
        bool fortDamagedState; 

        public DamageFortCommand(Fort fort) => this.fort = fort;

        public override void Do(IAction action) 
        {
            fortDamagedState = fort.damaged; 
            fort.damaged = true;
        }

        public override void Undo() => fort.damaged = fortDamagedState; 
    }
}
