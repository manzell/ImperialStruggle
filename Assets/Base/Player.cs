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

    [Button]
    public bool CanAffordAction(PlayerAction action)
    {
        ActionPoints apCosts = new ActionPoints(action.actionPointCost);
        ActionPoints playerAPs = new ActionPoints(actionPoints);
        
        foreach (ActionPoint apCost in apCosts.Where(ap => ap.baseValue > 0))
        {
            foreach (ActionPoint playerAP in playerAPs.Where(ap => ap.Value(action) > 0))
            {
                if(playerAP >= apCost) // note this is an actionPoint comparison which returns true if the first arg is eligible to pay for the 2nd
                {
                    int amtToCharge = Mathf.Min(playerAP.Value(action), apCost.baseValue);

                    apCost.baseValue -= amtToCharge;
                    playerAP.baseValue -= amtToCharge;
                }
            }

            if (apCost.baseValue > 0)
                return false; 
        }

        return true; 
    }
}
