using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 
using System.Linq;

namespace ImperialStruggle
{
    public class UI_Player : MonoBehaviour
    {
        public Player player;
        [SerializeField] Dictionary<GameObject, MinistryCardData> ministryCards = new ();
        [SerializeField] GameObject ministerCardArea, handArea;
        [SerializeField] Image flag;
        [SerializeField] GameObject cardPrefab, ministryCardPrefab;
        [SerializeField] UI_SelectionWindow selectionPrefab;

        public static System.Action<Faction> setFactionEvent;

        public UI_SelectionWindow Select<T>(Selection<T> selection) where T : ISelectable
        {
            UI_SelectionWindow window = Instantiate(selectionPrefab, transform);
            window.Open(selection);

            return window;
        }

        public void Awake()
        {
            player.SetUI(this);
            SelectMinistryCardCommand.SelectEvent += AddMinistryCard; 
        }

        void AddMinistryCard(MinistryCardData card)
        {
            if (card.Faction == player.Faction)
            {
                GameObject newMinistryCard = Instantiate(ministryCardPrefab, ministerCardArea.transform);
                newMinistryCard.GetComponent<UI_MinisterCard>().SetMinistryCard(card);
                ministryCards.Add(newMinistryCard, card);
            }
        }

        public void OnDealEventCard(Player player, EventCard card)
        {
            if (this.player == player)
                Instantiate(cardPrefab, handArea.transform).GetComponent<UI_Card>().SetCard(card);
        }
    }
}