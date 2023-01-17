using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class AddAPCommand : Command
    {
        Player player;
        [SerializeField] ActionPoint apAward; 

        public AddAPCommand(Player player, ActionPoint AP)
        {
            apAward = AP;
            this.player = player;
        }

        public override void Do(IAction context)
        {
            player.ActionPoints.Credit(apAward);

            Debug.Log("Add Action Point Event Invoked");

            player.ActionPoints.AdjustAPEvent.Invoke(); 
        }
    }
}
