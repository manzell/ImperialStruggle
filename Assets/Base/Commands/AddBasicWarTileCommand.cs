using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class AddBasicWarTileCommand : Command
{
    public override void Do(Action action)
    {
        if(action is WarPrepAction warAction)
        {
            Player player = Player.players[warAction.faction];
            WarTile warTile = player.basicWarTiles.OrderBy(tile => Random.value).First();

            if (player.basicWarTiles.Remove(warTile)) // Removing these from the Basic War Tile pool! Must remember to pull them back out after a war is scored
            {
                if (warAction.theater.warTiles.TryGetValue(warAction.faction, out List<WarTile> tiles))
                    warAction.theater.warTiles[warAction.faction].Add(warTile);
                else
                    warAction.theater.warTiles.Add(warAction.faction, new List<WarTile> { warTile });
            }

            Game.Log($"{warAction.faction} {warTile} added to {warAction.theater}");
        }
    }
}