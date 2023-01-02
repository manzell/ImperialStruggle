using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class CourtOfTheSunKingAction : MinisterAction
    {
        [SerializeField] Map map; 

        protected override Task Do()
        {
            ScoreMapAction.scoreMapEvent += BonusVP;
            return Task.CompletedTask; 
        }

        void BonusVP(Map map, AwardTile tile)
        {
            if (map == this.map && map.controllingFaction == Game.France)
                Commands.Push(new AdjustVPCommand(Game.France, 1)); 
        }
    }
}
