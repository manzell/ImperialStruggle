using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class DealInvestmentTileCommand : Command
{
    public static UnityEvent<InvestmentTile> dealInvestmentTileEvent = new UnityEvent<InvestmentTile>();
    public static UnityEvent<InvestmentTile> secondEvent = new UnityEvent<InvestmentTile>(); 

    public override void Do(BaseAction action)
    {
        if(action is ITargetType<InvestmentTile> tileAction)
        {
            InvestmentTile tile = tileAction.target;
            tile.status = InvestmentTile.InvestmentTileStatus.Available;
            Phase.currentPhase.GetComponent<PeaceTurn>().investmentTiles.Add(tile, Game.Faction.Neutral); 

            Game.Log($"{tile.name} added to Investment Tile Pool");
            dealInvestmentTileEvent.Invoke(tile);
            secondEvent.Invoke(tile); 
        }
    }
}