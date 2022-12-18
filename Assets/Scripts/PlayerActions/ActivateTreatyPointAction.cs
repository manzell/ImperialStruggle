using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class ActivateTreatyPointAction : PlayerAction
    {
        public ActionPoints actionPointAward;
        public ActionPoints actionPoints => actionPointAward;
        public Faction faction => Player.Faction;
        public int tp => -1;

        protected override Task Do()
        {
            return Task.CompletedTask; 
        }
    }
}