using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class ActivateTreatyPoint : GameAction
{
    public static UnityEvent<ActivateTreatyPoint> activateTreatyPointsEvent = new UnityEvent<ActivateTreatyPoint>();
    int amount;
    RecordsTrack recordsTrack;

    public ActivateTreatyPoint(Game.Faction faction, int amount)
    {
        recordsTrack = GameObject.FindObjectOfType<RecordsTrack>();
        actingFaction = faction;
        this.amount = amount;

        Do(this);
    }

    public void Do(ActivateTreatyPoint atp)
    {
        if (recordsTrack.treatyPoints[atp.actingFaction] >= atp.amount)
        {
            recordsTrack.treatyPoints[atp.actingFaction] -= atp.amount;
            Dictionary<Game.ActionType, int> actionPoints = Player.players[atp.actingFaction].majorActionPoints;

            if (actionPoints.ContainsKey(Game.ActionType.Treaty))
                actionPoints[Game.ActionType.Treaty] += atp.amount;
            else
                actionPoints.Add(Game.ActionType.Treaty, atp.amount);

            Debug.Log($"{atp.actingFaction} activates {atp.amount} Treaty {(atp.amount == 1 ? "Point" : "Points")}"); 
            activateTreatyPointsEvent.Invoke(atp);
        }
    }
}