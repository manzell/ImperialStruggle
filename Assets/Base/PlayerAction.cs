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
        Debug.Log($"Player Action: {this.name}");
        Debug.Log(base.Can());
        Debug.Log(player);
        Debug.Log(player.CanAffordAction(this));

        bool retVal = base.Can() && player.CanAffordAction(this);
        return retVal;
    }
}