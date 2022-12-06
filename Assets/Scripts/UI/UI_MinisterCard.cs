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
    [SerializeField] Image highlight; 

    public void OnPointerClick(PointerEventData eventData)
    {
        if(ministryCard.ministryCardStatus == MinistryCard.MinistryCardStatus.Revealed)
        {
        }
        else if(ministryCard.ministryCardStatus == MinistryCard.MinistryCardStatus.Selected)
        {
            ministryCard.ministryCardStatus = MinistryCard.MinistryCardStatus.Revealed;
            Debug.Log($"{ministryCard.faction} reveals {ministerName}."); 
        }
    }

    public void SetMinistryCard(MinistryCard card)
    {
        ministryCard = card;
        Style(); 
    }

    void Style()
    {
        ministerName.text = ministryCard.name;
        ministerName.color = ministryCard.ministryCardStatus == MinistryCard.MinistryCardStatus.Exhausted ? Color.gray : Color.black;

        switch(ministryCard.ministryCardStatus)
        {
            case MinistryCard.MinistryCardStatus.Selected:
                highlight.gameObject.SetActive(true);
                highlight.color = Color.gray;
                break;
            case MinistryCard.MinistryCardStatus.Exhausted:
                highlight.gameObject.SetActive(true);
                highlight.color = Color.black; 
                break;
            case MinistryCard.MinistryCardStatus.Revealed:
                highlight.gameObject.SetActive(false); 
                break;
        }
    }
}
