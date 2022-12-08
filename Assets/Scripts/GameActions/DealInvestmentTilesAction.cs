using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.Tilemaps;
using Sirenix.Utilities;
using ImperialStruggle;

public class DealInvestmentTilesAction : GameAction
{
    [SerializeField] int numToDeal;

    protected override void Do()
    {
        if (Phase.CurrentPhase is PeaceTurn peaceTurn)
        {
            commands.Add(new ResetInvestmentTilesCommand()); 

            Stack<InvestmentTile> tiles = new Stack<InvestmentTile>(FindObjectsOfType<InvestmentTile>()
                .OrderBy(tile => tile.status != InvestmentTile.InvestmentTileStatus.Reserve).ThenBy(tile => Random.value));

            for (int i = 0; i < numToDeal; i++)
            {
                if (tiles.ElementAt(i).status != InvestmentTile.InvestmentTileStatus.Reserve)
                    tiles.ForEach(tile => tile.status = InvestmentTile.InvestmentTileStatus.Reserve);

                commands.Add(new DealInvestmentTileCommand(tiles.ElementAt(i)));
            }
        }
    }
}