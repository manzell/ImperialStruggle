using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class AddAPCommand : Command
    {
        Player player;
        ActionPoint apAward; 
        ActionPoints apsAward;
        ActionPoints prevAP; 

        public AddAPCommand(Player player, ActionPoints AP)
        {
            apsAward = AP; 
            this.player = player;
        }

        public AddAPCommand(Player player, ActionPoint AP)
        {
            apAward = AP;
            this.player = player;
        }

        public override void Do(GameAction action)        
        {
            prevAP = player.ActionPoints;

            if (apAward != null)
                foreach (ActionPoint ap in apsAward)
                    player.ActionPoints.Credit(ap);

            Debug.Log("Add Action Point Event Invoked");

            player.ActionPoints.AdjustAPEvent.Invoke(); 
        }
    }
}
