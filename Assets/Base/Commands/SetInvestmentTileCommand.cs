using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetInvestmentTileCommand : Command
{
    public static UnityEvent<InvestmentTile, Game.Faction> setInvestmentTileEvent = new UnityEvent<InvestmentTile, Game.Faction>();
    Game.Faction undoFaction; 

    public override void Do(BaseAction action)
    {
        if(action is SelectInvestmentTileAction selectAction)
        {
            undoFaction = Phase.currentPhase.GetComponentInParent<PeaceTurn>().investmentTiles[selectAction.investmentTile]; 
            Phase.currentPhase.GetComponentInParent<PeaceTurn>().investmentTiles[selectAction.investmentTile] = selectAction.player.faction;

            setInvestmentTileEvent.Invoke(selectAction.investmentTile, selectAction.player.faction);
        }
    }
}
