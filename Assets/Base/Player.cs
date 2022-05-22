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
    public List<WarTile> warTiles, bonusWarTiles;

    public static Dictionary<Game.Faction, Player> players = new Dictionary<Game.Faction, Player>();

    private void Awake()
    {
        players.Add(faction, this);
        RecordsTrack.currentDebt.Add(faction, 0);
        RecordsTrack.debtLimit.Add(faction, 0);
        RecordsTrack.treatyPoints.Add(faction, 0);

        Phase.phaseEndEvent.AddListener(phase => actionPoints = new ActionPoints()); 
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

    public List<Squadron> squadrons;

    [SerializeField] PlayerAction currentAction; 
    [SerializeField] Dictionary<ActionPoint.ActionPointKey, int> totalAPCost = new Dictionary<ActionPoint.ActionPointKey, int>();
    [SerializeField] Dictionary<ActionPoint.ActionPointKey, List<ActionPoint>> eligibleAPs = new Dictionary<ActionPoint.ActionPointKey, List<ActionPoint>>();

    public bool CanAffordAction(PlayerAction action)
    {
        /* The player can use the matching Minor, or Major, or Treaty/Debt Action Points
         * 
         * we have player.actionPoints and action.actionPointsCost, both ActionPoints
         * 
         * First, we take each ActionPointCost AP, then loop through the player Action Points. 
         * If an individual action point can pay for the cost, we add it to the list of eligible APs to use for the action
         */

        // First things first - build our bucket of AP costs by AP type: 
        currentAction = action;
        totalAPCost.Clear();
        eligibleAPs.Clear(); 

        foreach (ActionPoint actionAP in action.actionPointCost)
        {
            ActionPoint.ActionPointKey APKey = actionAP.apKey;

            if (totalAPCost.ContainsKey(APKey))
            {
                totalAPCost[APKey] += actionAP.Value(action);
            }
            else
            {
                totalAPCost.Add(APKey, actionAP.Value(action));
                eligibleAPs.Add(APKey, new List<ActionPoint>()); 
            }

            // Now we go through and add any eligible player APs and assign them to their category. 
            foreach (ActionPoint playerAP in action.player.actionPoints.Where(ap => !eligibleAPs[APKey].Contains(ap))
                .OrderBy(ap => ap.actionTier).ThenBy(ap => ap.actionType == ActionPoint.ActionType.Debt || ap.actionType == ActionPoint.ActionType.Treaty))
            {
                if((playerAP.actionType == actionAP.actionType || playerAP.actionType == ActionPoint.ActionType.Debt || playerAP.actionType == ActionPoint.ActionType.Treaty) && 
                    playerAP.actionTier >= actionAP.actionTier && playerAP.Value(action) > 0)
                {
                    eligibleAPs[APKey].Add(playerAP); 
                }
            }
        }

        // Now we go through each thing in our totalAPCost, sum the values in the matching eligibleAPs table, and see if they're worth more 

        //foreach(KeyValuePair<ActionPoint.ActionPointKey, int> ap in totalAPCost)
        //{
        //    Debug.Log($"Cost: {ap.Key} {ap.Value}");
        //    Debug.Log($"Available: {eligibleAPs[ap.Key].Sum(actionPoint => actionPoint.Value(action))}");
        //    Debug.Log(ap.Value >= eligibleAPs[ap.Key].Sum(actionPoint => actionPoint.Value(action))); 
        //}

        return totalAPCost.All(ap => eligibleAPs[ap.Key].Sum(actionPoint => actionPoint.Value(action)) >= ap.Value); 

    }
}
