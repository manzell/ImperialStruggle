using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceTreatyPointsCommand : Command
{
    [SerializeField] int treatyPointsCap = 4; 

    public override void Do(BaseAction action)
    {
        RecordsTrack recordsTrack = GameObject.FindObjectOfType<RecordsTrack>();

        foreach(Game.Faction faction in recordsTrack.treatyPoints.Keys)
            recordsTrack.treatyPoints[faction] = Mathf.Min(recordsTrack.treatyPoints[faction], treatyPointsCap); 
    }
}
