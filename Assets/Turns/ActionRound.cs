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
        SelectInvestmentTile.selectInvestmentTileEvent.AddListener(SetInvestmentTile); 
    }

    public void SetInvestmentTile(InvestmentTile tile)
    {
        if(investmentTile == null && Phase.currentPhase == this)
        {
            investmentTile = tile;
            gameActions.Add(new AdjustActionPoints(actingFaction, tile.actionPoints)); // Why does this live in the ActionRound? Put somewhere else
        }
    }
}
