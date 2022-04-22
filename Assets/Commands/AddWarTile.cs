using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWarTile : Command
{
    WarTile tile;
    Theater theater;
    public Game.Faction targetFaction;

    public override void Do(Action action)
    {
        theater.warTiles.Add(tile);
    }
}