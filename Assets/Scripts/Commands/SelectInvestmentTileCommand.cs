using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SelectInvestmentTileCommand : Command
{
    InvestmentTile tile;
    Faction faction; 

    public SelectInvestmentTileCommand(InvestmentTile tile, Faction faction)
    {
        this.tile = tile;
        this.faction = faction;
    }

    public override void Do(GameAction action)
    {
        if(Phase.CurrentPhase is PeaceTurn peaceTurn)
            peaceTurn.investmentTiles.Add(tile, faction); 
    }

    public override void Undo()
    {
        if(Phase.CurrentPhase is PeaceTurn peaceTurn)
            peaceTurn.investmentTiles.Remove(tile);

    }
}
