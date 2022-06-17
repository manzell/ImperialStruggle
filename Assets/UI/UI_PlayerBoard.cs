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

    public static UnityEvent<Game.Faction> setFactionEvent = new UnityEvent<Game.Faction>();

    GraphicSettings graphicSettings;

    public void Awake()
    {
        graphicSettings = FindObjectOfType<Game>().graphicSettings;
        SelectMinistryCardCommand.selectMinistryCardEvent.AddListener(card => { if (card.faction == player.faction) AddMinistryCard(card); });
        DealCardCommand.dealCardEvent.AddListener(card => { if (player.hand.Contains(card)) AddEventCard(card); });
    }

    void AddMinistryCard(MinistryCard card)
    {
        GameObject newMinistryCard = Instantiate(ministryCardPrefab, ministerCardArea.transform);        
        newMinistryCard.GetComponent<UI_MinisterCard>().SetMinistryCard(card);
        ministryCards.Add(newMinistryCard, card); 
    }

    public void AddEventCard(EventCard card) => Instantiate(cardPrefab, handArea.transform).GetComponent<UI_Card>().SetCard(card);
}