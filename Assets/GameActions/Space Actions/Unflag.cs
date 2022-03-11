using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

public class Unflag : Action
{
    public List<Space> eligibleSpaces = new List<Space>();

    [Button]
    public void UnflagSpace(Space space)
    {
        if (space is PoliticalSpace == false && space is Market == false) return;
        if (space.flag == actingFaction || space.flag == Game.Faction.Neutral) return; 

        Phase.currentPhase.gameActions.Add(new ShiftSpace(space, actingFaction));
    }
}
