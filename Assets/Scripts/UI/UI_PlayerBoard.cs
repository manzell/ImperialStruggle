using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 
using System.Linq; 

public class UI_PlayerBoard : MonoBehaviour
{
    public Player player; 
    [SerializeField] Dictionary<GameObject, MinistryCard> ministryCards = new Dictionary<GameObject, MinistryCard>();
    [SerializeField] GameObject ministerCardArea, handArea;
    [SerializeField] Image flag;
    [SerializeField] GameObject cardPrefab, ministryCardPrefab;

    public static UnityEvent<Faction> setFactionEvent = new UnityEvent<Faction>();

    public void Awake()
    {
        SelectMinistryCardCommand.selectMinistryCardEvent += AddMinistryCard;
        DealCardCommand.dealCardEvent += AddEventCard;
    }

    void AddMinistryCard(MinistryCard card)
    {
        if (card.data.faction == player.faction)
        {
            GameObject newMinistryCard = Instantiate(ministryCardPrefab, ministerCardArea.transform);
            newMinistryCard.GetComponent<UI_MinisterCard>().SetMinistryCard(card);
            ministryCards.Add(newMinistryCard, card);
        }
    }

    public void AddEventCard(Player player, EventCard card)
    {
        if(player.hand.Contains(card))
            Instantiate(cardPrefab, handArea.transform).GetComponent<UI_Card>().SetCard(card);
    }
}