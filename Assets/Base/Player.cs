using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq; 

public class Player : SerializedMonoBehaviour, ISelectable
{
    public Game.Faction faction; 
    public List<EventCard> hand;
    public List<MinistryCard> ministers; // bool = revealed?
    public ActionPoints actionPoints = new ActionPoints(); 
    public List<WarTile> basicWarTiles, bonusWarTiles;

    public static Dictionary<Game.Faction, Player> players = new Dictionary<Game.Faction, Player>();

    private void Awake()
    {
        players.Add(faction, this);
    }

    public List<Game.Keyword> keywords
    {
        get
        {
            List<Game.Keyword> _keywords = new List<Game.Keyword>();
            foreach (MinistryCard card in ministers)
                foreach(Game.Keyword _keyword in card.keywords)                    
                    if(!_keywords.Contains(_keyword))
                        _keywords.Add(_keyword);

            return _keywords;
        }
    }

    public bool CanAffordAction(PlayerAction action)
    {
        Dictionary<ActionPoint.ActionPointKey, int> availableActionPoints = new Dictionary<ActionPoint.ActionPointKey, int>();

        foreach (ActionPoint ap in actionPoints)
        {
            ActionPoint.ActionPointKey apKey = new ActionPoint.ActionPointKey(ap.actionType, ap.actionTier);

            if (availableActionPoints.ContainsKey(apKey))
                availableActionPoints[apKey] += ap.Value(action);
            else
                availableActionPoints.Add(apKey, ap.Value(action));
        }

        // Note this presently fails to include Major Action Points in affording minor actions
        return action.actionPointCost.All(cost => availableActionPoints[new ActionPoint.ActionPointKey(cost.actionType, cost.actionTier)] >= cost.Value(action));
    }
}
