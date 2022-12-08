using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

public class Game : SerializedMonoBehaviour
{
    public static System.Action startGameEvent;

    public static List<EventCard> eventDeck = new List<EventCard>(), eventDiscards = new List<EventCard>();
    public static Dictionary<SpaceData, Space> SpaceLookup { get; private set; } = new();
    public static HashSet<Space> Spaces { get; private set; }

    public static Faction Neutral { get; private set; }
    public static Faction Britain { get; private set; }
    public static Faction France { get; private set; }
    public static Faction Spain { get; private set; }
    public static Faction USA { get; private set; }

    [SerializeField] HashSet<Resource> Resources;
    [SerializeField] Faction britain, france, usa, spain, neutral;
    [SerializeField] Phase startingPhase;
    [SerializeField] HashSet<SpaceData> startingSpaces;

    public static GlobalDemandTrack GlobalDemandTrack { get; private set; }
    [SerializeField] GlobalDemandTrack globalDemandTrack;

    public GraphicSettings graphicSettings;

    private void Awake()
    {
        France = france;
        Britain = britain;
        USA = usa;
        Spain = spain;
        Neutral = neutral;

        GlobalDemandTrack = globalDemandTrack; 

        LoadSpaces(startingSpaces); 
    }

    private void Start()
    {
        startingPhase?.StartPhase(); 
    }

    private void LoadSpaces(IEnumerable<SpaceData> spaces)
    {
        Spaces = new(); 

        foreach (MarketData data in spaces.OfType<MarketData>())
            Spaces.Add(new Market(data));

        foreach (PoliticalData data in spaces.OfType<PoliticalData>())
            Spaces.Add(new PoliticalSpace(data));

        foreach (FortData data in spaces.OfType<FortData>())
            Spaces.Add(new Fort(data));

        foreach (TerritoryData data in spaces.OfType<TerritoryData>())
            Spaces.Add(new Territory(data));

        foreach (NavalData data in spaces.OfType<NavalData>())
            Spaces.Add(new NavalSpace(data));

        // Now go back through and set the Adjacent Spaces using our Runtime Space Classes
        foreach (Space space in Spaces)
        {
            SpaceLookup.Add(space.data, space); 
            space.adjacentSpaces.AddRange(space.data.adjacentSpaces.Select(spacedata => SpaceLookup[spacedata]));
        }
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

        victoryPointState = RecordsTrack.VictoryPoints;
    }
}