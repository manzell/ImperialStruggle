using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using Sirenix.Utilities;

namespace ImperialStruggle
{
    public class SamuelJohnsonAction : MinisterAction
    {
        [SerializeField] Map map;

        protected override Task Do(IAction context)
        {
            ScoreMapAction.scoreMapEvent += BonusVP;
            return Task.CompletedTask;
        }

        void BonusVP(Map map, AwardTile tile)
        {
            if(this.map == map)
            {
                if (map.controllingFaction == Game.France && tile.VP > 0)
                    Commands.Push(new AdjustVPCommand(Game.France, -1));
                else if (map.controllingFaction == Game.Britain)
                    Commands.Push(new AdjustVPCommand(Game.Britain, 1)); 
            }
        }
    }
}