using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagFortCommand : Command
{
    public override void Do(BaseAction action)
    {
        if (action is IPlayerAction playerAction && action is ITargetType<Fort> fort)
        {
            fort.target.flag = playerAction.player.faction;
            Debug.Log($"{playerAction.player} flags {fort.target.name}");
        }
    }
}
