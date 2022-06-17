using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetTreatyPointsAction : GameAction, IAdjustTP
{
    [SerializeField] int treatyPointsCap = 4;

    public Game.Faction faction => _faction;
    Game.Faction _faction;

    public int tp => _tp;
    int _tp;

    public override void Do(UnityAction callback)
    {
        foreach(Game.Faction faction in Player.players.Keys)
        {
            _faction = faction;
            _tp = Mathf.Min(0, treatyPointsCap - RecordsTrack.treatyPoints[_faction]);
            base.Do(() => { });
        }

        callback.Invoke(); 
    }
}
