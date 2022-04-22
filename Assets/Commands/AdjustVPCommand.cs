using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AdjustVPCommand : Command
{
    public Calculation<int> adjustAmount;
    public static UnityEvent<AdjustVPCommand> adjustVPEvent = new UnityEvent<AdjustVPCommand>();
    public Game.Faction targetFaction;

    public override void Do(Action action)
    {
        if (targetFaction == Game.Faction.Britain || targetFaction == Game.Faction.France)
        {
            Debug.Log($"{targetFaction} victory points increased by {Mathf.Abs(adjustAmount.value)}");
            VictoryPointTrack.VP += targetFaction == Game.Faction.Britain ? -adjustAmount.value : adjustAmount.value;

            adjustVPEvent.Invoke(this);
        }
    }

    public override void Undo() => VictoryPointTrack.VP -= adjustAmount.value;
}