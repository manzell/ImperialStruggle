using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;

public class Game : MonoBehaviour
{
    public enum Era { Succession, Empire, Revolution } // Move these to Phase
    public enum Keyword { Style, Governance, Mercantilism, Scholarship, Finance } // Move to MinistryCard
    public enum ActionType { None, Finance, Diplomacy, Military, Debt, Treaty, Free, VictoryPoint } // move these to ActionPoint
    public enum ActionTier { Minor, Major }

    [field: SerializeField] public readonly List<Resource> Resources;

    public static List<EventCard> eventDeck = new List<EventCard>(), eventDiscards = new List<EventCard>();

    public static Dictionary<PoliticalSpace, UI_PoliticalSpace> PoliticalSpaces;
    public static Dictionary<Market, UI_MarketSpace> Markets;
    public static Dictionary<Fort, UI_Fort> Forts;
    public static Dictionary<Territory, UI_Territory> Territories;
    public static Dictionary<NavalSpace, UI_NavalSpace> NavalSpaces;
    public static Dictionary<Space, UI_Space> Spaces; 

    public static Faction Britain { get; private set; }
    public static Faction France { get; private set; }
    public static Faction Spain { get; private set; }
    public static Faction USA { get; private set; }
    public static Faction Neutral { get; private set; }
    [SerializeField] Faction britain, france, usa, spain, neutral;

    [SerializeField] Phase startingPhase;

    public GlobalDemandTrack globalDemandTrack;
    public GraphicSettings graphicSettings;

    public static System.Action startGameEvent;

    private void Awake()
    {
        France = france;
        Britain = britain;
        USA = usa;
        Spain = spain;
        Neutral = neutral; 
    }

    private void Start()
    {
        startingPhase?.StartPhase(); 
    }

    static List<string> gamelog = new List<string>();
    public static void Log(string str)
    {
        print(str);
        gamelog.Add(str);
    }

    public static Phase NextWarPhase => NextWarTurn.GetComponent<Phase>();

    public static WarTurn NextWarTurn
    {
        get
        {
            List<Phase> allPhases = Phase.rootPhase.GetComponentsInChildren<Phase>().ToList();
            int currentPhaseIndex = allPhases.IndexOf(Phase.CurrentPhase);

            foreach (WarTurn war in Phase.rootPhase.GetComponentsInChildren<WarTurn>())
                if (allPhases.IndexOf(war.GetComponent<Phase>()) > currentPhaseIndex)
                    return war;

            return null;
        }
    }

    public static UnityEvent<Player> setActivePlayerEvent = new UnityEvent<Player>(); 
}

public struct GameState
{
    public Phase CurrentPhase;
    public Dictionary<Faction, ActionPoints> actionPointState;
    public Dictionary<Faction, (int debt, int debtLimit)> debtState;
    public Dictionary<Faction, int> treatyPointState;
    public Dictionary<AdvantageTile, AdvantageTile.AdvantageTileState> advantageTileState;
    public Dictionary<EventCard, Player> eventCardStates;
    public Dictionary<MinistryCard, MinistryCard.MinistryCardStatus> ministryCardsState;
    public Dictionary<Space, Faction> spaceStates;
    public Dictionary<Squadron, Space> squadronState;

    public int victoryPointState;

    public GameState(Phase phase)
    {
        CurrentPhase = phase;
        actionPointState = new();
        debtState = new();
        treatyPointState = new();
        eventCardStates = new();
        squadronState = new();
        advantageTileState = new(); 
        ministryCardsState = new();
        spaceStates = new();

        foreach (Player player in GameObject.FindObjectsOfType<Player>())
        {
            actionPointState.Add(player.faction, player.actionPoints);
            debtState.Add(player.faction, (RecordsTrack.currentDebt[player.faction], RecordsTrack.debtLimit[player.faction]));
            treatyPointState.Add(player.faction, RecordsTrack.treatyPoints[player.faction]);

            foreach (EventCard card in player.hand)
                eventCardStates.Add(card, player);

            foreach (Squadron squadron in player.squadrons)
                squadronState.Add(squadron, squadron.space);
        }

        foreach (AdvantageTile tile in GameObject.FindObjectsOfType<AdvantageTile>())
            advantageTileState.Add(tile, tile.tileState);

        foreach (MinistryCard card in GameObject.FindObjectsOfType<MinistryCard>())
            ministryCardsState.Add(card, card.ministryCardStatus);

        /*
        foreach (Space space in GameObject.FindObjectsOfType<Space>())
            spaceStates.Add(space, space.flag);
        */

        victoryPointState = RecordsTrack.VictoryPoints;
    }
}