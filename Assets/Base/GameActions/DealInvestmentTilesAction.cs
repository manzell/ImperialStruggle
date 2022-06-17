using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class DealInvestmentTilesAction : GameAction, ITargetType<InvestmentTile>
{
    InvestmentTile tile;
    [SerializeField] int numToDeal;

    public InvestmentTile target => tile;

    public override void Do(UnityAction callback)
    {
        List<InvestmentTile> tiles = FindObjectsOfType<InvestmentTile>().ToList();

        for (int i = 0; i < numToDeal; i++)
        {
            if(tiles.Count(tile => tile.status == InvestmentTile.InvestmentTileStatus.Reserve) == 0)
                foreach(InvestmentTile tile in tiles.Where(tile => tile.status == InvestmentTile.InvestmentTileStatus.Exhausted))
                    tile.status = InvestmentTile.InvestmentTileStatus.Reserve;

            if (tiles.Count(tile => tile.status == InvestmentTile.InvestmentTileStatus.Reserve) > 0)
                tile = tiles.OrderBy(tile => Random.value).First(tile => tile.status == InvestmentTile.InvestmentTileStatus.Reserve);
            else
                break; // This should never happen as long as we remember to manage our Investment Tiles during the Reset Phase

            base.Do(() => { });
        }

        callback.Invoke(); 
    }
}