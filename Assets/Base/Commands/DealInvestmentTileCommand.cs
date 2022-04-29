using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealInvestmentTileCommand : Command
{
    public override void Do(Action action)
    {
        if(action is DealInvestmentTilesAction tileAction)
        {
            tileAction.tile.status = InvestmentTile.InvestmentTileStatus.Available;
            Game.Log($"{tileAction.tile} added to Investment Tile Pool");
        }
    }
}
