using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class WarPrepAction : GameAction
{
    public WarTurn war;
    [HideInInspector] public WarTile tile;
    [HideInInspector] public Game.Faction faction;
    [HideInInspector] public Theater theater;

    Game.Faction[] factions = { Game.Faction.France, Game.Faction.Britain };

    public override void Do(UnityAction callback)
    {
        foreach (Theater _theater in war.theaters)
        {
            foreach (Game.Faction _faction in Player.players.Keys)
            {
                faction = _faction;
                theater = _theater;
                tile = Player.players[faction].warTiles.OrderBy(tile => Random.value).First();
                base.Do(() => { });
            }
        }

        callback.Invoke(); 
    }
}