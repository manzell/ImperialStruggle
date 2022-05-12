using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class AddWarTileToTheaterCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is WarPrepAction warAction)
        {
            Dictionary<Game.Faction, List<WarTile>> warTiles = warAction.theater.warTiles; 

            if (warTiles.ContainsKey(warAction.faction))
                warTiles[warAction.faction].Add(warAction.tile);
            else
                warTiles.Add(warAction.faction, new List<WarTile> { warAction.tile });

            Player.players[warAction.faction].basicWarTiles.Remove(warAction.tile); // Remember to add these back into the basicWarTiles dictionary later!

            Game.Log($"{warAction.faction} {warAction.tile} added to {warAction.theater}");
        }
    }
}