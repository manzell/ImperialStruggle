using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;

public class PlayerAction : BaseAction
{
    public Player player;
    public ActionPoints actionPointCost;

    protected override void Do(UnityAction callback)
    {
        if (Can())
            commands.ForEach(command => command.Do(this));
    }

    public override bool Can()
    {
        bool retVal = base.Can() && player.CanAffordAction(this);
        return retVal;
    }
}