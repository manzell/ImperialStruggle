using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class WarPrepAction : GameAction
{
    public WarTurn war;
    [HideInInspector] public Game.Faction faction;
    [HideInInspector] public Theater theater;
    public WarTile tile; 

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

    void Visit_AddBasicWarTileCommand(AddWarTileToTheaterCommand command)
    {
        command.theater = theater; 
        command.tile = Player.players[faction].basicWarTiles.OrderBy(tile => Random.value).First();
        command.faction = faction; 
    }
}

public interface Visitor
{
    public void Visit_AddBasiceWarTileCommand(AddWarTileToTheaterCommand command); 
}