using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreMapAction : GameAction, IAdjustTP, IAdjustVP
{
    public Dictionary<Map, Game.Faction> mapWinners = new Dictionary<Map, Game.Faction>();
    public static UnityEvent<Map> scoreMapEvent; 

    public Game.Faction faction => _faction;
    public int tp => _tp;
    public int vp => _vp;

    int _tp, _vp;
    Game.Faction _faction;

    public override void Do(UnityAction callback)
    {
        foreach(Map map in mapWinners.Keys)
        {
            if(map.controllingFaction != Game.Faction.Neutral)
            {
                _faction = map.controllingFaction;
                mapWinners[map] = _faction;
                _vp = map.awardTile.victoryPoints;
                _tp = map.awardTile.treatyPoints;
                map.awardTile.used = false; 
                base.Do(() => { });
                scoreMapEvent.Invoke(map); 
            }
        }

        callback.Invoke(); 
    }
}
