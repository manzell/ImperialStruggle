using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ImperialStruggle
{
    public class ScoreMapAction : GameAction
    {
        public static UnityEvent<Map> scoreMapEvent;

        protected override void Do()
        {
            throw new System.NotImplementedException(); 
            /*
            foreach(Map map in mapWinners.Keys)
            {
                if(map.controllingFaction != Game.Neutral)
                {
                    _faction = map.controllingFaction;
                    mapWinners[map] = _faction;
                    _vp = map.awardTile.victoryPoints;
                    _tp = map.awardTile.treatyPoints;
                    map.awardTile.used = false; 
                    scoreMapEvent.Invoke(map); 
                }
            }
            */
        }
    }
}