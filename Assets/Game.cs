using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class Game : MonoBehaviour
{
    public enum Faction { Neutral, France, Britain, Spain, USA }
    public enum Era { Succession, Empire, Revolution }
    public enum Keyword { Style, Governance, Mercantilism, Scholarship, Finance }
    public enum ActionType { None, Finance, Diplomacy, Military, Debt, Treaty, Free, VictoryPoint }
    public enum ActionTier { Minor, Major }
    public enum Resource { Fur, Fish, Tobacco, Sugar, Cotton, Spices }

    public static List<EventCard> eventDeck = new List<EventCard>(), eventDiscards = new List<EventCard>();
    public static List<Player> players; 
    public static Faction initiative = Faction.France;
    public static GlobalDemandTrack GlobalDemand => FindObjectOfType<Game>().globalDemandTrack;

    public Phase firstPhase; 

    public GlobalDemandTrack globalDemandTrack; 
    public GraphicSettings graphicSettings; 

    private void Awake()
    {
        players = FindObjectsOfType<Player>().ToList();
        firstPhase?.StartThread(); 
    }

    static List<string> gamelog = new List<string>(); 
    public static void Log(string str)
    {
        print(str);
        gamelog.Add(str); 
    }
}
