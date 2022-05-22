using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
using TMPro; 

public class UI_MinisterCard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] MinistryCard ministryCard;
    [SerializeField] TextMeshProUGUI ministerName;

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void Style(MinistryCard card)
    {
        ministryCard = card;
        ministerName.text = ministryCard.name; 
    }
}
