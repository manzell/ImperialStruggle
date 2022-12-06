using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class ActionRound : Phase
{
    public Faction actingFaction;
    public InvestmentTile investmentTile { get; private set; }
    bool actionRoundCompleted; 

    // The Action Round only completes when the player signals that they have completed it via a Button Press
    public override bool Completed => actionRoundCompleted;

}
