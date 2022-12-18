using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class NextPhaseCommand : Command
    {
        Phase lastPhase; 
        public override void Do(GameAction action)
        {
            lastPhase = Phase.CurrentPhase;
            Phase.CurrentPhase.Advance();
        }

        public override void Undo()
        {
            Phase.CurrentPhase = lastPhase; 
        }
    }
}