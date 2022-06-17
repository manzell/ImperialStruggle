using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AdjustDebtLimitAction : GameAction
{
    [HideInInspector] public Game.Faction faction;
    [SerializeField] List<Game.Faction> factions;
    public int amount;

    public override void Do(UnityAction callback)
    {
        foreach(Game.Faction _faction in factions)
        {
            faction = _faction; 
            base.Do(() => { });
        }

        callback.Invoke();
    }
}