using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class ScoreGlobalDemandAction : GameAction, IAdjustVP, IAdjustTP, IAdjustDebt
{
    Game.Faction _faction; 
    public Game.Faction faction => _faction;

    int _vp = 0, _tp = 0, _debt = 0;
    int IAdjustVP.vp => _vp;
    int IAdjustTP.tp => _tp;
    int IAdjustDebt.debt => _debt;

    public Dictionary<Game.Resource, Game.Faction> globalDemandWinners = new Dictionary<Game.Resource, Game.Faction>();

    protected override void Do(UnityAction callback)
    {
        GlobalDemandTrack demandTrack = Game.GlobalDemand; 

        if(TryGetComponent(out PeaceTurn peaceTurn))
        {
            Game.Era era = GetComponent<Phase>().era; 

            foreach(Game.Resource resource in peaceTurn.globalDemandResources)
            {
                // We don't need to know the resource - just the VP, Treaty Points, and Debt awards. 
                // We're going to set them (AND the faction). 

                int britainCount = FindObjectsOfType<Market>().Where(market => market.flag == Game.Faction.Britain && market.marketType == resource).Count();
                int franceCount = FindObjectsOfType<Market>().Where(market => market.flag == Game.Faction.France && market.marketType == resource).Count();

                if (britainCount > franceCount)
                    _faction = Game.Faction.Britain;
                else if (franceCount > britainCount)
                    _faction = Game.Faction.France;
                else 
                    _faction = Game.Faction.Neutral;

                globalDemandWinners.Add(resource, _faction);

                if (_faction != Game.Faction.Neutral)
                {
                    _vp = demandTrack[era][resource][ActionPoint.ActionType.VictoryPoint];
                    _tp = demandTrack[era][resource][ActionPoint.ActionType.Treaty];
                    _debt = demandTrack[era][resource][ActionPoint.ActionType.Debt];

                    base.Do(() => { });
                }
            }
        }

        callback.Invoke(); 
    }
}