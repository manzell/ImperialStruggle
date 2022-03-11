using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SelectInvestmentTile : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        InvestmentTile tile = GetComponent<UI_InvestmentTile>().tile;

        if (tile.available && Phase.currentPhase.TryGetComponent(out SelectInvestmentTilePhase tilePhase) && Phase.currentPhase is ActionRound && (Phase.currentPhase as ActionRound).investmentTile == null)
            tilePhase.Select(tile);
    }
}
