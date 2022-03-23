using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Linq; 

public class UI_SelectMinistryCard : MonoBehaviour
{
    [SerializeField] Image actingFlag;
    [SerializeField] GameObject cardPrefab, cardsGO, cardSelectorWindow;
    [SerializeField] TMPro.TextMeshProUGUI windowTitle; 
    [SerializeField] Button submitButton; 
    List<ICard> displayedCards = new List<ICard>();
    List<ICard> selectedCards = new List<ICard>();
    Dictionary<ICard, GameObject> cards = new Dictionary<ICard, GameObject>();

    public int numToSelect = 1; 

    private void Awake()
    {
        SelectMinistersPhase.selectMinisterPhaseEvent.AddListener(OnMinistryPhaseStart);
        SelectMinistersPhase.selectMinistersEvent.AddListener(OnSelectMinisters);
        SelectMinistersPhase.endMinisterPhaseEvent.AddListener(x => Close()); 
    }

    public void Select(ICard card)
    {
        if(selectedCards.Contains(card))
        {
            selectedCards.Remove(card);
            cards[card].GetComponent<UI_Card>().RemoveHighlight(); 
        }
        else if (selectedCards.Count < numToSelect)
        {
            selectedCards.Add(card);
            cards[card].GetComponent<UI_Card>().SetHighlight(Color.green); 
        }
    }

    public void Close()
    {
        cardSelectorWindow.SetActive(false);
    }

    void OnMinistryPhaseStart(SelectMinistersPhase phase)
    {
        selectedCards.Clear();
        displayedCards.Clear();
        cards.Clear(); 
        // Open Our Window
        cardSelectorWindow.SetActive(true);

        // For now, assume England goes first
        DisplayMinistryCards(phase, UI_PlayerBoard.faction);
    }

    public void DisplayMinistryCards(SelectMinistersPhase phase, Game.Faction faction)
    {
        actingFlag.sprite = FindObjectOfType<Game>().graphicSettings.flags[faction];
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(() => {
            phase.Select(faction, selectedCards.Cast<MinistryCard>().ToList());
        });
        windowTitle.text = "Set Minister Cards"; 

        // Set up our available cards        
        List<ICard> ministryCards = FindObjectsOfType<MinistryCard>()
            .Where(card => card.faction == faction && card.eras.Contains(Phase.currentPhase.era))
            .OrderBy(card => card.name)
            .ToList<ICard>();

        SetCards(ministryCards, 2);
    }

    void OnSelectMinisters(SelectMinistersPhase phase, Game.Faction faction, List<MinistryCard> cards)
    {
        // Get Rid of Any Existing Cards
        selectedCards.Clear();
        displayedCards.Clear();
        this.cards.Clear(); 

        foreach (Transform child in cardsGO.transform)
            Destroy(child.gameObject);

        if (faction == Game.Faction.England)
            DisplayMinistryCards(phase, Game.Faction.France);
        else if (faction == Game.Faction.France)
            DisplayMinistryCards(phase, Game.Faction.England);
    }

    [Button]
    public void SetCards(List<ICard> cards, int count)
    {
        selectedCards.Clear();
        this.cards.Clear();

        numToSelect = count;
        displayedCards = cards; 

        foreach(ICard icard in cards)
        {
            GameObject c = Instantiate(cardPrefab, cardsGO.transform);            
            UI_Card uicard = c.GetComponent<UI_Card>();
            uicard.gameObject.AddComponent<UI_ClickSelectCard>(); 

            c.name = icard.gameObject.name;

            this.cards.Add(icard, c); 
            uicard.SetCard(icard); 
        }
    }
}
