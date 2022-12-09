using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetAwardTiles : GameAction
{
    [SerializeField] List<Map> maps; 
    protected override void Do()
    {
        if(Phase.CurrentPhase is PeaceTurn peaceTurn)
        {
            Queue<AwardTile> awardTiles = new Queue<AwardTile>(GameObject.FindObjectsOfType<AwardTile>().OrderBy(tile => tile.used).ThenBy(tile => Random.value).Take(4));

            foreach (Map map in Game.Spaces.Select(space => space.map)) 
                commands.Add(new SetAwardTileCommand(map, awardTiles.Dequeue()));

            if (!awardTiles.Any(tile => tile.used == false))
                awardTiles.ForEach(tile => tile.used = false);
        }            
    }
}