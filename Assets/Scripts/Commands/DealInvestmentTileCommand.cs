using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class DealInvestmentTileCommand : Command
{
    public static UnityEvent<InvestmentTile> dealInvestmentTileEvent = new UnityEvent<InvestmentTile>();

    public InvestmentTile tile; 
    public DealInvestmentTileCommand(InvestmentTile tile) => this.tile = tile;

    public override void Do(GameAction action)
    {
        if(Phase.CurrentPhase is PeaceTurn peaceTurn)
        {
            peaceTurn.investmentTiles.Add(tile, null); 
            tile.status = InvestmentTile.InvestmentTileStatus.Available;

            Debug.Log($"{tile.name} added to Investment Tile Pool");
            dealInvestmentTileEvent.Invoke(tile);
        }
    }
}