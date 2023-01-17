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
            int adjustment = 0;

            if (map.controllingFaction == Game.France)
                adjustment = -1;
            else if (map.controllingFaction == Game.Britain)
                adjustment = 1; 

            if (map == this.map)
            {
                Debug.Log("WARNING, THIS PERMANENTLY INCREASES THE VP Value, Fix!!"); 
                ActionPoint ap = tile.ActionPoints.Where(t => t.type == ActionPoint.ActionType.VictoryPoint).First();                
                ap.AdjustBaseValue(adjustment); 
            }

        }
    }
}