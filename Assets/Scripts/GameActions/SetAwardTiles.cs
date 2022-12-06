using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetAwardTiles : GameAction
{
    protected override void Do()
    {
        Queue<AwardTile> tiles = new(FindObjectsOfType<AwardTile>().Where(tile => tile.used == false).OrderBy(tile => Random.value)); 

        if(tiles.Count() == 0)
        {
            FindObjectsOfType<AwardTile>().ForEach(tile => tile.used = false);
            tiles = new(FindObjectsOfType<AwardTile>().Where(tile => tile.used == false).OrderBy(tile => Random.value));
        }

        foreach(Map map in FindObjectsOfType<Map>())
            commands.Push(new SetAwardTileCommand(map, tiles.Dequeue()));
    }
}