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

        if (tile.available && actionRound.investmentTile == null)
            tile.Select(actionRound.actingFaction); 
    }
}
