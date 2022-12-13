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

        GameObject modalWindow; 

        public static UnityEvent<Faction> setFactionEvent = new UnityEvent<Faction>();

        public void Awake()
        {
            player.UI = this;
            // Move these out of their Commands? Or Encapsulate it there?
            SelectMinistryCardCommand.SelectEvent += AddMinistryCard;
            DealCardCommand.dealCardEvent += OnDealEventCard;
        }

        void AddMinistryCard(MinistryCardData card)
        {
            if (card.faction == player.faction)
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

        public void Select<T>(Selection<T> selection) where T: ISelectable
        {
            UI_SelectionWindow window = Instantiate(selectionPrefab, transform);
            window.transform.position = new Vector3(0, 0, 0);
            window.Open(selection);
        }
    }
}