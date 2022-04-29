using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SelectInvestmentTile : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) =>
        InvestmentTile.selectInvestmentTileEvent.Invoke(GetComponent<UI_InvestmentTile>().tile); 
}
