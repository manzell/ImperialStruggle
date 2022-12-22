using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
using TMPro;

namespace ImperialStruggle
{
    public class UI_MinisterCard : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] MinistryCardData ministryCard;
        [SerializeField] TextMeshProUGUI ministerName;
        [SerializeField] Image highlight;

        Dictionary<MinistryCardData, MinistryCard.MinistryCardStatus> ministers => ministryCard.Faction.player.Ministers;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (ministers[ministryCard] == MinistryCard.MinistryCardStatus.Revealed)
            {
            }
            else if (ministers[ministryCard] == MinistryCard.MinistryCardStatus.Selected)
            {
                ministryCard.Faction.player.Ministers[ministryCard] = MinistryCard.MinistryCardStatus.Revealed;
                Debug.Log($"{ministryCard.Faction} reveals {ministerName}.");
            }
        }

        public void SetMinistryCard(MinistryCardData card)
        {
            ministryCard = card;
            Style();
        }

        void Style()
        {
            ministerName.text = ministryCard.Name;
            ministerName.color = ministryCard.Faction.player.Ministers[ministryCard] == MinistryCard.MinistryCardStatus.Exhausted ? Color.gray : Color.black;

            switch (ministers[ministryCard])
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
}