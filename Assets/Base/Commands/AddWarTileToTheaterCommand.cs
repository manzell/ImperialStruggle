using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class AddWarTileToTheaterCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is WarPrepAction warAction) // Deprecate This
        {
            Dictionary<Game.Faction, List<WarTile>> warTiles = warAction.theater.warTiles; 

            if (warTiles.ContainsKey(warAction.faction))
                warTiles[warAction.faction].Add(warAction.tile);
            else
                warTiles.Add(warAction.faction, new List<WarTile> { warAction.tile });

            Player.players[warAction.faction].warTiles.Remove(warAction.tile); 

            Game.Log($"{warAction.faction} {warAction.tile} added to {warAction.theater}");
        }
        if(action is PlayerAction playerAction && action is ITargetType<Theater> theater && action is ITargetType<WarTile> warTile)
        {
            if (!theater.target.warTiles[playerAction.player.faction].Contains(warTile.target))
            {
                theater.target.warTiles[playerAction.player.faction].Add(warTile.target);
                playerAction.player.warTiles.Remove(warTile.target);

                Game.Log($"{playerAction.player} {warTile.target.tileName} added to {theater.target}");
            }
        }
    }
}