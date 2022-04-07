using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SelectInvestmentTile : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        InvestmentTile tile = GetComponent<UI_InvestmentTile>().tile;
        ActionRound actionRound = Phase.currentPhase as ActionRound; 

        if (tile.available && actionRound.investmentTile == null) // heads up ALL this should do is trigger the event. Adjusting Action Points should be handled elsewhere - maybe inside the InvestmentTile itself
        {
            actionRound.GetComponent<SelectInvestmentTilePhase>().Select(tile); // Select Investment Tile Phase might go away in the future. 
            actionRound.gameActions.Add(new AdjustActionPoints(actionRound.actingFaction, tile.actionPoints)); // This should be a respondent to the SelectInvestmentTile event
        }
    }
}
