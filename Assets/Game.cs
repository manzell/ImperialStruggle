using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class Game : MonoBehaviour
{
    public enum Faction { Neutral, France, England, Spain, USA }
    public enum Era { Succession, Empire, Revolution }
    public enum Keyword { Style, Governance, Mercantilism, Scholarship, Finance }
    public enum ActionType { None, Finance, Diplomacy, Military, Debt, Treaty, Free, VictoryPoint }
    public enum ActionTier { Minor, Major }
    public enum Resource { Fur, Fish, Tobacco, Sugar, Cotton, Spices }

    public static List<EventCard> eventDeck = new List<EventCard>(), eventDiscards = new List<EventCard>();
    public static List<Player> players; 

    public static Faction initiative = Faction.France;

    public GraphicSettings graphicSettings; 

    private void Awake()
    {
        players = FindObjectsOfType<Player>().ToList(); 
    }

    public static bool CanAfford(Dictionary<(ActionType, ActionTier), int> cost, Dictionary<(ActionType, ActionTier), int> resources)
    {
        bool retVal = true; 

        foreach(KeyValuePair<(ActionType type, ActionTier tier), int> kvp in cost)
        {
            if(kvp.Key.tier == Game.ActionTier.Minor)
            {
                // Minor Action Point. We need to have a total of Major and Minor Action Points in the current Type, less the cost of this action AND less than any major action tier cost
                resources.TryGetValue((kvp.Key.type, ActionTier.Minor), out int minorActionPoints);
                resources.TryGetValue((kvp.Key.type, ActionTier.Major), out int majorActionPoints);
                cost.TryGetValue((kvp.Key.type, ActionTier.Major), out int majorActionCost);
                cost.TryGetValue((kvp.Key.type, ActionTier.Minor), out int minorActionCost);

                retVal &= majorActionPoints >= majorActionCost &&
                    majorActionPoints + minorActionPoints - majorActionCost >= minorActionCost; 
            }
            else if(kvp.Key.tier == ActionTier.Major)
            {
                resources.TryGetValue((kvp.Key.type, ActionTier.Major), out int majorActionPoints);
                cost.TryGetValue((kvp.Key.type, ActionTier.Major), out int majorActionCost);
                
                retVal &= majorActionPoints >= majorActionCost;
            }
        }

        return retVal; 
    }
}
