using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;

public class PlayerAction : BaseAction
{
    public Player player;
    public ActionPoints actionPointCost;

    public override bool Can() => player.CanAffordAction(this) && base.Can();

    protected override void Do(UnityAction callback)
    {
        if (Can())
        {
            PayAPCommand payCommand = new PayAPCommand();
            payCommand.Do(this); 
            commands.ForEach(command => command.Do(this));
        }
    }
}