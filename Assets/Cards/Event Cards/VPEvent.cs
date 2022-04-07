using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VPEvent : CardEvent
{
    [SerializeField] int vpAward;

    public override void Event()
    {
        Phase.currentPhase.gameActions.Add(new AdjustVictoryPoints(faction, vpAward));
    }
}
