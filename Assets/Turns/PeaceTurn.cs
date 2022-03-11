using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaceTurn : Phase
{
    public Game.Faction initiative; 
    public List<ActionRound> actionRounds;
    public List<InvestmentTile> availableInvestmentTiles = new List<InvestmentTile>(), 
        usedInvestmentTiles = new List<InvestmentTile >();

    public List<Game.Resource> globalDemandResources = new List<Game.Resource>();
}
