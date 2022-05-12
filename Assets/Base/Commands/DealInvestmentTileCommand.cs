using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealInvestmentTileCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is DealInvestmentTilesAction tileAction)
        {
            tileAction.tile.status = InvestmentTile.InvestmentTileStatus.Available;
            Phase.currentPhase.GetComponent<PeaceTurn>().investmentTiles.Add(tileAction.tile, Game.Faction.Neutral); 
            Game.Log($"{tileAction.tile} added to Investment Tile Pool");
        }
    }
}
