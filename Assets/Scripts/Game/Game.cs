using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using ImperialStruggle;

public class Game : SerializedMonoBehaviour
{
    public static System.Action startGameEvent;

    public static Stack<EventCardData> EventDeck = new(), Discards = new ();
    public static Dictionary<SpaceData, Space> SpaceLookup { get; private set; }
    public static HashSet<Space> Spaces { get; private set; }
    public static IEnumerable<AwardTile> AwardTiles { get; private set; }
    public static IEnumerable<InvestmentTile> InvestmentTiles { get; private set; }

    public static Faction Neutral { get; private set; }
    public static Faction Britain { get; private set; }
    public static Faction France { get; private set; }
    public static Faction Spain { get; private set; }
    public static Faction USA { get; private set; }

    public static HashSet<Resource> Resources; 
    [SerializeField] List<AwardTile> awardTiles;
    [SerializeField] List<InvestmentTileData> investmentTiles;
    [SerializeField] HashSet<SpaceData> startingSpaces;
    [SerializeField] Faction britain, france, usa, spain, neutral;
    [SerializeField] Phase startingPhase;

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
        AwardTiles = awardTiles;
        InvestmentTiles = investmentTiles.Select(tile => new InvestmentTile(tile)); 
        Resources = new(startingSpaces.OfType<MarketData>().Where(market => market.resource != null).Select(market => market.resource));
        LoadSpaces(startingSpaces); 
    }

    private void Start()
    {
        startingPhase?.StartPhase(); 
    }

    private void LoadSpaces(IEnumerable<SpaceData> spaces)
    {
        Spaces = new();

        Spaces.AddRange(spaces.OfType<MarketData>().Select(data => new Market(data)));
        Spaces.AddRange(spaces.OfType<PoliticalData>().Select(data => new PoliticalSpace(data)));
        Spaces.AddRange(spaces.OfType<FortData>().Select(data => new Fort(data)));
        Spaces.AddRange(spaces.OfType<TerritoryData>().Select(data => new Territory(data)));
        Spaces.AddRange(spaces.OfType<NavalData>().Select(data => new NavalSpace(data)));

        // Now go back through and set the Adjacent Spaces using our Runtime Space Classes
        SpaceLookup = Spaces.ToDictionary(space => space.data); 

        foreach (Space space in Spaces)
            space.adjacentSpaces.AddRange(space.data.adjacentSpaces.Select(spacedata => SpaceLookup[spacedata]));
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

        // Advantage Tiles
        // Ministry Tiles


        victoryPointState = RecordsTrack.VictoryPoints;
    }
}