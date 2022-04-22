using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class SelectInvestmentTile : Command
{
    public static UnityEvent<InvestmentTile> selectInvestmentTileEvent = new UnityEvent<InvestmentTile>(); 
    public InvestmentTile investmentTile;

    public override void Do(Action action)
    {
        PeaceTurn peaceTurn = Phase.currentPhase.GetComponentInParent<PeaceTurn>();

        investmentTile.available = false;
        investmentTile.exhausted = true;
        
        //peaceTurn.availableInvestmentTiles.Remove(investmentTile);
        //peaceTurn.usedInvestmentTiles.Add(investmentTile);

        selectInvestmentTileEvent.Invoke(investmentTile);
    }

    public override void Undo()
    {
        PeaceTurn peaceTurn = Phase.currentPhase.GetComponentInParent<PeaceTurn>();
        //peaceTurn.availableInvestmentTiles.Add(investmentTile);
        //peaceTurn.usedInvestmentTiles.Remove(investmentTile); 

        investmentTile.available = true;
        investmentTile.exhausted = false;
    }
}
