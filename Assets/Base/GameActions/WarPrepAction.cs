using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class WarPrepAction : GameAction
{
    public WarTurn war;
    [HideInInspector] public Game.Faction faction;
    [HideInInspector] public Theater theater; 

    Game.Faction[] factions = { Game.Faction.France, Game.Faction.Britain };

    protected override void Do(UnityAction callback)
    {
        foreach (Theater _theater in war.theaters)
        {
            foreach (Game.Faction _faction in factions)
            {
                faction = _faction;
                theater = _theater;
                base.Do(() => { });
            }
        }

        callback.Invoke(); 
    }
}
