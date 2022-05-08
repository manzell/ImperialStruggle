using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class AddWarTileToTheaterCommand : Command
{
    public Theater theater;
    public WarTile tile;
    public Game.Faction faction; 

    //public override void Do(BaseAction action)
    public override void Do(BaseAction action)
    {
        if(action is WarPrepAction warAction)
        {
            if (Player.players[warAction.faction].basicWarTiles.Remove(warAction.tile)) // Removing these from the Basic War Tile pool! Must remember to pull them back out after a war is scored
            {
                if (warAction.theater.warTiles.ContainsKey(warAction.faction))
                    warAction.theater.warTiles[warAction.faction].Add(warAction.tile);
                else
                    warAction.theater.warTiles.Add(warAction.faction, new List<WarTile> { warAction.tile });
            }

            Game.Log($"{warAction.faction} {warAction.tile} added to {warAction.theater}");

        }
    }
}