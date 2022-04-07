using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPointEvent : CardEvent
{
    [SerializeField] List<Dictionary<(Game.ActionType, Game.ActionTier), int>> apAwards;
    [SerializeField] List<Space> eligibleSpaces;
    [SerializeField] int numAwards = 1; 

    public override void Event()
    {
        // TODO - Figure out how to restrict ActionPoints to certain criteria. 

        if(apAwards.Count == 1) 
            Phase.currentPhase.gameActions.Add(new AdjustActionPoints(faction, apAwards[0])); 
        else
        {
            Debug.Log($"Select {numAwards} of {apAwards.Count} AP Awards"); 
        }
    }
}
