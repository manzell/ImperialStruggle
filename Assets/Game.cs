using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;

public class Game : MonoBehaviour
{
    public enum Faction { Neutral, France, Britain, Spain, USA }
    public enum Era { Succession, Empire, Revolution } // Move these to Phase
    public enum Keyword { Style, Governance, Mercantilism, Scholarship, Finance } // Move to MinistryCard
    public enum ActionType { None, Finance, Diplomacy, Military, Debt, Treaty, Free, VictoryPoint } // move these to ActionPoint
    public enum ActionTier { Minor, Major }
    public enum Resource { Fur, Fish, Tobacco, Sugar, Cotton, Spices } // most to ScriptableObject? 

    public static List<EventCard> eventDeck = new List<EventCard>(), eventDiscards = new List<EventCard>();
    public static Player activePlayer; 
    public static Faction initiative = Faction.France;
    public static GlobalDemandTrack GlobalDemand => FindObjectOfType<Game>().globalDemandTrack;

    public Phase startPhaseOnGameLaunch;

    public GlobalDemandTrack globalDemandTrack;
    public GraphicSettings graphicSettings;

    public static UnityEvent startGameEvent = new UnityEvent(); 

    private void Start()
    {
        SetActivePlayer(Player.players[Faction.France]);
        startPhaseOnGameLaunch?.StartThread();
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
            int currentPhaseIndex = allPhases.IndexOf(Phase.currentPhase);

            foreach (WarTurn war in Phase.rootPhase.GetComponentsInChildren<WarTurn>())
                if (allPhases.IndexOf(war.GetComponent<Phase>()) > currentPhaseIndex)
                    return war;

            return null;
        }
    }

    public static UnityEvent<Player> setActivePlayerEvent = new UnityEvent<Player>(); 
    public static void SetActivePlayer(Player p)
    {
        activePlayer = p;
        setActivePlayerEvent.Invoke(p); 
    }
}
