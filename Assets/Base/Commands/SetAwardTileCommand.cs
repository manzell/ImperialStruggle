using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class SetAwardTileCommand : Command
{
    [SerializeField] Map map;     

    public override void Do(BaseAction action)
    {
        if(action.TryGetComponent(out PeaceTurn peaceTurn))
        {
            AwardTile tile = GameObject.FindObjectsOfType<AwardTile>()
                .Where(t => !peaceTurn.awardTiles.Values.Contains(t))
                .OrderBy(t => Random.value)
                .First(); 

            peaceTurn.awardTiles.Add(map, tile);
            Game.Log($"{tile} set as {map} Award Tile");
        }
    }
}
