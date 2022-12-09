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
            commands.Add(new ResetInvestmentTilesCommand()); // What does this even do??

            commands.Add(new DealInvestmentTileCommand(
                Game.InvestmentTiles.OrderBy(tile => tile.status != InvestmentTile.InvestmentTileStatus.Reserve).ThenBy(tile => Random.value).Take(numToDeal))); 
        }
    }
}