using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class SetAwardTileCommand : Command
{
    [SerializeField] Map map;
    [SerializeField] AwardTile tile;

    public SetAwardTileCommand(Map map, AwardTile tile)
    {
        this.map = map;
        this.tile = tile;
    }

    public override void Do(GameAction action)
    {
        if(Phase.CurrentPhase is PeaceTurn peaceTurn)
        { 
            peaceTurn.awardTiles.Add(map, tile);
            Debug.Log($"{tile} set as {map} Award Tile");
        }
    }
}
