using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq; 

public class Player : SerializedMonoBehaviour, ISelectable
{
    public Faction faction; 
    public List<EventCard> hand;
    public List<MinistryCard> ministers; // bool = revealed?
    public ActionPoints actionPoints = new ActionPoints(); 
    public Queue<WarTile> warTiles, bonusWarTiles;

    public static List<Player> players = new();  

    private void Awake()
    {
        players.Add(this);
        RecordsTrack.currentDebt.Add(faction, 0);
        RecordsTrack.debtLimit.Add(faction, 0);
        RecordsTrack.treatyPoints.Add(faction, 0);

        ActionRound.PhaseEndEvent += ResetActionPoints;

        foreach (WarTile warTile in GetComponentsInChildren<WarTile>().OrderBy(tile => Random.value))
        {
            if (warTile.warTileSet == WarTile.WarTileSet.Basic)
                warTiles.Enqueue(warTile);
            else if (warTile.warTileSet == WarTile.WarTileSet.Bonus)
                bonusWarTiles.Enqueue(warTile); 
        }
    }

    void ResetActionPoints(Phase phase) 
    {
        Debug.Log($"Resetting Action Points {phase}");
        actionPoints = new ActionPoints();
    }

    public HashSet<MinistryCard.Keyword> Keywords => new HashSet<MinistryCard.Keyword>(ministers.SelectMany(minister => minister.keywords)); 

    public List<Squadron> squadrons;

    [Button]
    public bool CanAffordAction(PlayerAction action)
    {
        return true; 
        /*
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
        */
    }
}
