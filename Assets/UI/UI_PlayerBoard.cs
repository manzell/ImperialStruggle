using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 
using System.Linq; 

public class UI_PlayerBoard : MonoBehaviour
{
    [SerializeField] List<GameObject> ministerCards;
    [SerializeField] GameObject ministerCardArea, handArea, permanentAbilities, ministerAbilities, advantageTiles;
    [SerializeField] Image flag;
    [SerializeField] GameObject cardPrefab, actionPrefab;

    public static UnityEvent<Game.Faction> setFactionEvent = new UnityEvent<Game.Faction>();

    public static Game.Faction faction = Game.Faction.France; 

    public void Start()
    {
        SetFaction(faction);
    }



    public void SetFaction(Game.Faction faction)
    {
        GraphicSettings graphicSettings = FindObjectOfType<Game>().graphicSettings;
        UI_PlayerBoard.faction = faction; 

        setFactionEvent.Invoke(faction);

        SetMinisters(Player.players[faction].ministers);
        SetCards(Player.players[faction].hand); 
        flag.sprite = graphicSettings.flags[faction];
    }

    void SetMinisters(List<MinistryCard> ministers) => ministers.Take(2).ToList().ForEach(minister => {
        UI_Card uiCard = ministerCards[ministers.IndexOf(minister)].GetComponent<UI_Card>();
        uiCard.SetCard(minister);

        if (minister.ministryCardStatus < MinistryCard.MinistryCardStatus.Revealed)
            uiCard.SetHighlight(Color.black); 
    });

    public void SetCards(List<EventCard> cards)
    {
        foreach (Transform child in handArea.transform)
            Destroy(child.gameObject);

        foreach (EventCard card in cards)
            AddCard(card); 
    }

    public void HilightCards(InvestmentTile tile)
    {

    }

    public void RemoveCardHighlights() =>
        handArea.GetComponentsInChildren<UI_Card>().ToList().ForEach(card =>
        {
            card.RemoveHighlight();
            Destroy(card.GetComponent<UI_ClickPlayCard>());
        });

    public void AddCard(EventCard card)
    {
        GameObject c = Instantiate(cardPrefab, handArea.transform);
        c.GetComponent<UI_Card>().SetCard(card); 
    }

    public void RemoveCard(EventCard card)
    {
        foreach(Transform child in handArea.transform)
            if ((EventCard)child.GetComponent<UI_Card>()?.card == card)
                Destroy(child.gameObject); 
    }
}