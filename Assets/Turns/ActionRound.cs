using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class ActionRound : Phase
{
    public Game.Faction actingFaction;
    public InvestmentTile investmentTile;

    private void Awake()
    {
        SelectInvestmentTilePhase.selectInvestmentTileEvent.AddListener(SetInvestmentTile); 
    }

    public void SetInvestmentTile(Game.Faction faction, InvestmentTile tile)
    {
        investmentTile = tile;
        gameActions.Add(new AdjustActionPoints(faction, tile.majorAction, tile.minorAction)); // Why does this live in the ActionRound? Put somewhere else
    }
}
