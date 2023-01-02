using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

namespace ImperialStruggle
{
    public class UI_MinisterCard : MonoBehaviour, IPopupMenu
    {
        public MinistryCard Minister { get; private set; }
        [SerializeField] TextMeshProUGUI ministerName;
        [SerializeField] Image highlight;

        public void SetMinistryCard(MinistryCard card)
        {
            Minister = card;
            Style();
        }

        void Style()
        {
            ministerName.text = Minister.Name;
            ministerName.color = Minister.ministryCardStatus == MinistryCard.MinistryCardStatus.Exhausted ? Color.gray : Color.black;

            switch (Minister.ministryCardStatus)
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

        public void OpenPopupMenu()
        {
            switch (Minister.ministryCardStatus)
            {
                case MinistryCard.MinistryCardStatus.Selected:
                    UI_PopupMenu.Open(new List<IPlayerAction>() { new RevealAction(Minister, Minister.data.Faction.player) });
                    break;
                case MinistryCard.MinistryCardStatus.Revealed:
                    UI_PopupMenu.Open(Minister.data.MinisterActions); 
                    break;
                case MinistryCard.MinistryCardStatus.Exhausted:
                    break;
                case MinistryCard.MinistryCardStatus.Reserved:
                    break;
            }
        }
    }
}