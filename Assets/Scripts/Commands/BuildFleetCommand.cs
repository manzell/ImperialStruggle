using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class BuildFleetCommand : Command
    {
        Player player;
        public BuildFleetCommand(Player player) => this.player = player; 

        public override void Do(IAction context) => player.Squadrons.Add(new Squadron()); 
    }
}