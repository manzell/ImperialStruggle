using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class JohnLawAction : PlayerAction
{
    [SerializeField] List<Space> scotlandSpaces = new List<Space>();

    public override void Do(UnityAction callback)
    {
        foreach (Command command in commands.Where(_command => _command is AdjustDebtCommand))
        {
            (command as AdjustDebtCommand).amount = -Mathf.Min(scotlandSpaces.Any(space => space.flag == player.faction) ? 2 : 1, RecordsTrack.currentDebt[player.faction]);
            (command as AdjustDebtCommand).faction = player.faction;
        }

        base.Do(callback);
    }
}
