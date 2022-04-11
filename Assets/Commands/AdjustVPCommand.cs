using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AdjustVPCommand : Command
{
    public Calculation<int> adjustAmount;
    public static UnityEvent<AdjustVPCommand> adjustVPEvent = new UnityEvent<AdjustVPCommand>();

    public override void Do(Game.Faction faction)
    {
        if (faction == Game.Faction.Britain || faction == Game.Faction.France)
        {
            Debug.Log($"{targetFaction} victory points increased by {Mathf.Abs(adjustAmount.value)}");
            VictoryPointTrack.VP += faction == Game.Faction.Britain ? -adjustAmount.value : adjustAmount.value;

            base.Do(faction);
            adjustVPEvent.Invoke(this);
        }
    }

    public override void Undo() => VictoryPointTrack.VP -= adjustAmount.value;
}