using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReduceTreatyPointsPhase : MonoBehaviour
{
    public int treatyPointsCap = 4;
    RecordsTrack recordsTrack; 

    public void Do(Phase phase, UnityAction callback)
    {
        recordsTrack = FindObjectOfType<RecordsTrack>();
        foreach(KeyValuePair<Game.Faction, int> pair in recordsTrack.treatyPoints)
        {
            if (recordsTrack.treatyPoints[pair.Key] > treatyPointsCap)
            {
                //AdjustTPCommand adjustTPCommand = phase.gameObject.AddComponent<AdjustTPCommand>();
                //adjustTPCommand.targetFaction = pair.Key;
                //adjustTPCommand.adjustAmount.value = treatyPointsCap - recordsTrack.treatyPoints[pair.Key];
                //adjustTPCommand.Do(); 
            }
        }

        callback.Invoke(); 
    }
}