using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetTreatyPointsAction : GameAction, IScoreTP
{
    [SerializeField] int treatyPointsCap = 4;

    Game.Faction IScoreTP.faction => throw new System.NotImplementedException();
    Game.Faction _faction;

    public int tp => _tp;
    int _tp;



    protected override void Do(UnityAction callback)
    {
        foreach(Game.Faction faction in Player.players.Keys)
        {
            _faction = faction;
            _tp = Mathf.Min(0, treatyPointsCap - FindObjectOfType<RecordsTrack>().treatyPoints[_faction]);
            base.Do(() => { });
        }

        callback.Invoke(); 
    }
}