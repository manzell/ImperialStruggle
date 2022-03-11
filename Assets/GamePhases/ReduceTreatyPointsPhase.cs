using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReduceTreatyPointsPhase : MonoBehaviour, IPhaseAction
{
    public int treatyPointsCap = 4;

    public void Do(Phase phase, UnityAction callback)
    {
        foreach(KeyValuePair<Game.Faction, int> pair in RecordsTrack.treatyPoints)
        {
            if (RecordsTrack.treatyPoints[pair.Key] >= treatyPointsCap)
            {
                AdjustTreatyPoints atp = new AdjustTreatyPoints(pair.Key, treatyPointsCap - RecordsTrack.treatyPoints[pair.Key]);
                (phase as ActionRound).gameActions.Add(atp);
            }
        }

        callback.Invoke(); 
    }
}