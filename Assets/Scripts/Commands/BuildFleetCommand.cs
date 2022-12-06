using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFleetCommand : Command
{
    public override void Do(GameAction action)
    {
        //This command adds a fleet to the Navy

        if(action is IPlayerAction playerAction)
        {
            Squadron squadron = new Squadron();
            NavyBox.squadrons.Add(squadron);

            squadron.flag = playerAction.player.faction;
            playerAction.player.squadrons.Add(squadron); 

            Debug.Log($"{playerAction.player} adds a Squadron to the Naval Box");
        }
    }
}
