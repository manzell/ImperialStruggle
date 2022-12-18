using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class NextPhaseAction : GameAction
    {
        protected override Task Do()
        {
            Commands.Push(new NextPhaseCommand());
            return Task.CompletedTask;
        }
    }
}
