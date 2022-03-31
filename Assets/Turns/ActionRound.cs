using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class ActionRound : Phase
{
    public Game.Faction actingFaction;
    public InvestmentTile investmentTile;

    private void Awake() => SelectInvestmentTile.selectInvestmentTileEvent.AddListener(SetInvestmentTile);
    public void SetInvestmentTile(InvestmentTile tile)
    {
        if(currentPhase == this && investmentTile == null)
        {
            investmentTile = tile;
            SelectInvestmentTile.selectInvestmentTileEvent.RemoveListener(SetInvestmentTile);
        }
    }
}
