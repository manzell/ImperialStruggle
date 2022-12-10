using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class DealInvestmentTilesAction : GameAction
    {
        [SerializeField] int numToDeal;

        protected override void Do()
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
            {
                commands.Add(new ResetInvestmentTilesCommand()); // What does this even do??

                IEnumerable<InvestmentTile> tiles = Game.InvestmentTiles.OrderBy(tile => tile.Value != PeaceTurn.InvestmentTileStatus.Reserve).ThenBy(tile => Random.value)
                    .Select(kvp => kvp.Key).Take(numToDeal);

                commands.Add(new DealInvestmentTileCommand(tiles)); 
                    
            }
        }
    }
}