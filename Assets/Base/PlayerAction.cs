using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq; 

public class PlayerAction : BaseAction
{
    public Player player;
    public ActionPoints actionPointCost; 
    UnityAction callback; 

    protected override void Do(UnityAction callback)
    {   
        this.callback = callback;

        if (Can())
            commands.ForEach(command => command.Do(this));
    }

    public override bool Can() => base.Can() && actionPointCost.Count > 0 ? player.CanAffordAction(this) : true;
}