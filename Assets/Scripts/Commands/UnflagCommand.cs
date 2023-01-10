using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class UnflagCommand : Command
    {
        FlaggableSpace space; 
        public UnflagCommand(FlaggableSpace space) => this.space = space;

        public override void Do(GameAction action) => space.SetFlag(null);
    }
}
