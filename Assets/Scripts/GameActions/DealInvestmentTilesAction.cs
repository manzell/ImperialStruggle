using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class DealInvestmentTilesAction : GameAction
    {
        [SerializeField] int numToDeal;

        protected override Task Do()
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
            {
                //Commands.Push(new ResetInvestmentTilesCommand()); // What does this even do??

                IEnumerable<InvestmentTile> tiles = Game.InvestmentTiles.OrderBy(tile => tile.Value != InvestmentTile.InvestmentTileStatus.Reserve).ThenBy(tile => Random.value)
                    .Select(kvp => kvp.Key).Take(numToDeal);

                Commands.Push(new DealInvestmentTileCommand(tiles));
            }

            return Task.CompletedTask; 
        }
    }
}