using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace ImperialStruggle
{
    public class DealInvestmentTileCommand : Command
    {
        public static UnityEvent<InvestmentTile> dealInvestmentTileEvent = new UnityEvent<InvestmentTile>();

        IEnumerable<InvestmentTile> tiles;
        public DealInvestmentTileCommand(IEnumerable<InvestmentTile> tiles) => this.tiles = tiles;

        public override void Do(GameAction action)
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
            {
                foreach (InvestmentTile tile in tiles)
                {
                    peaceTurn.investmentTiles.Add(tile, null);
                    dealInvestmentTileEvent.Invoke(tile);
                    Debug.Log($"{tile.Name} added to Investment Tile Pool");
                }
            }
        }
    }
}