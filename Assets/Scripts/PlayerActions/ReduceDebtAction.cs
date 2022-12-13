using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class ReduceDebtAction : PlayerAction
    {
        public int debtAdjustment;
        protected override void Do() => Commands.Push(new AdjustDebtCommand(player.faction, debtAdjustment));
    }
}