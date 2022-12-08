using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class ResetInvestmentTilesCommand : Command
    {
        public override void Do(GameAction action)
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
                peaceTurn.investmentTiles.Clear(); 
        }
    }
}
