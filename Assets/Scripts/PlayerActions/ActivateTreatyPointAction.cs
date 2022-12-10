using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class ActivateTreatyPointAction : PlayerAction
    {
        public ActionPoints actionPointAward;
        public ActionPoints actionPoints => actionPointAward;
        public Faction faction => actingPlayer.faction;
        public int tp => -1;

        protected override void Do()
        {
            throw new System.NotImplementedException();
        }
    }
}