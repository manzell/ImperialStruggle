using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VPEvent : CardEvent
{
    [SerializeField] int vpAward;

    public override void Event()
    {
        AdjustVPCommand adjustVictoryPoints = Phase.currentPhase.gameObject.AddComponent<AdjustVPCommand>();
        adjustVictoryPoints.adjustAmount.value = vpAward;
        Phase.currentPhase.gameActions.Add(adjustVictoryPoints);
    }
}
