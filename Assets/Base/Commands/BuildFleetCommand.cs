using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFleetCommand : Command
{
    public override void Do(BaseAction action)
    {
        //This command adds a fleet to the Navy

        if(action is ITargetType<Player> player)
        {
            Squadron squadron = new Squadron();
            squadron.flag = player.target.faction; 
        }
    }
}
