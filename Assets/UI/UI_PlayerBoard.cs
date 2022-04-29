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

    private void Awake()
    {
        //SelectMinistersPhase.selectMinistersEvent.AddListener((phase, faction, ministers) => { if (UI_PlayerBoard.faction == faction) SetMinisters(ministers); });
        PlayCard.playCardEvent.AddListener(playCard => RemoveCard(playCard.card));
        PlayCard.playCardEvent.AddListener(playCard => RemoveCardHighlights());
        // Remove Highlight on Play Card Event as well as any other Action? 
    }

    public void Start()
    {
        SetFaction(faction);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
            SetFaction(faction == Game.Faction.Britain ? Game.Faction.France : Game.Faction.Britain); 
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
        //if(tile.GetComponent<EventCardTriggerCommand>())
        //{
        //    // Issue this only highlights the cards if we're looking at the correct player's Player Board. If I'm tabbed over it won't highlight. 
        //    // To fix I'll need to have two player boards, 1 for each player, and transition them, rather than the current approach. 
        //    handArea.GetComponentsInChildren<UI_Card>()
        //        .Where(uicard => (uicard.card as EventCard).reqdActionType == Game.ActionType.None || 
        //            (uicard.card as EventCard).reqdActionType == tile.majorActionType)
        //        .ToList()
        //        .ForEach(card => {
        //            card.GetComponent<UI_Card>().SetHighlight(Color.green);
        //            if(!card.GetComponent<UI_ClickPlayCard>())
        //                card.gameObject.AddComponent<UI_ClickPlayCard>(); 
        //        }); 
        //}
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
