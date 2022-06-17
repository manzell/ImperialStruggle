using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectInvestmentTileCommand : Command
{
    public static UnityEvent<InvestmentTile, Game.Faction> selectInvestmentTileEvent = new UnityEvent<InvestmentTile, Game.Faction>();
    Game.Faction undoFaction; 

    public override void Do(BaseAction action)
    {
        if(action is SelectInvestmentTileAction selectAction)
        {
            undoFaction = Phase.currentPhase.GetComponentInParent<PeaceTurn>().investmentTiles[selectAction.investmentTile]; 
            Phase.currentPhase.GetComponentInParent<PeaceTurn>().investmentTiles[selectAction.investmentTile] = selectAction.player.faction;

            Debug.Log($"{selectAction.player.name} selects {selectAction.investmentTile.name} Investment Tile"); 
            selectInvestmentTileEvent.Invoke(selectAction.investmentTile, selectAction.player.faction);
        }
    }
}
