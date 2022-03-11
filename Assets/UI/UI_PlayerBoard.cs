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

    public Game.Faction faction = Game.Faction.England; 

    private void Awake()
    {
        SelectMinistersPhase.selectMinistersEvent.AddListener((phase, faction, ministers) => { if (this.faction == faction) SetMinisters(ministers); });
        DealCardsPhase.dealCardEvent.AddListener((faction, card) => { if (faction == this.faction) AddCard(card); });
    }

    public void Start()
    {
        SetFaction(faction);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
            SetFaction(faction == Game.Faction.England ? Game.Faction.France : Game.Faction.England); 
    }

    public void SetFaction(Game.Faction faction)
    {
        GraphicSettings graphicSettings = FindObjectOfType<Game>().graphicSettings;
        this.faction = faction; 

        setFactionEvent.Invoke(faction);

        SetMinisters(Player.players[faction].ministers);
        SetCards(Player.players[faction].hand); 
        flag.sprite = graphicSettings.flags[faction];
    }

    void SetMinisters(List<MinistryCard> ministers) => ministers.Take(2).ToList().ForEach(minister => {
        UI_Card uiCard = ministerCards[ministers.IndexOf(minister)].GetComponent<UI_Card>();
        uiCard.SetCard(minister);

        if (minister.revealed == false)
            uiCard.SetHighlight(Color.black); 
    });

    public void SetCards(List<EventCard> cards)
    {
        foreach (Transform child in handArea.transform)
            Destroy(child.gameObject);

        foreach (EventCard card in cards)
            AddCard(card); 
    }

    public void AddCard(EventCard card)
    {
        GameObject c = Instantiate(cardPrefab, handArea.transform);
        c.GetComponent<UI_Card>().SetCard(card); 
    }

    public void RemoveCard(EventCard card)
    {
        foreach(Transform child in handArea.transform)
        {
            if(child.TryGetComponent(out UI_Card c))
                if((EventCard)c.card == card)
                    Destroy(card.gameObject);
        }
    }
}
