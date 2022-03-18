using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReduceTreatyPointsPhase : MonoBehaviour, IPhaseAction
{
    public int treatyPointsCap = 4;
    RecordsTrack recordsTrack; 

    public void Do(Phase phase, UnityAction callback)
    {
        recordsTrack = FindObjectOfType<RecordsTrack>();
        foreach(KeyValuePair<Game.Faction, int> pair in recordsTrack.treatyPoints)
        {
            if (recordsTrack.treatyPoints[pair.Key] >= treatyPointsCap)
            {
                AdjustTreatyPoints atp = new AdjustTreatyPoints(pair.Key, treatyPointsCap - recordsTrack.treatyPoints[pair.Key]);
                (phase as ActionRound).gameActions.Add(atp);
            }
        }

        callback.Invoke(); 
    }
}