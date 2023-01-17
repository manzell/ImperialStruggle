using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class AdjustCPCostCommand : Command
    {
        Territory territory;
        int amount;

        public AdjustCPCostCommand(Territory territory, int amount)
        {
            this.territory = territory;
            this.amount = amount;
        }

        public override void Do(IAction action)
        {
            territory.CPCost += amount;
        }
    }
}
