using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class RemoveWarTileCommand : Command
{
    [SerializeField] Calculation<Phase> targetWar;
    [SerializeField] int numTiles;
    [SerializeField] Game.Faction targetFaction;

    public override void Do(BaseAction action)
    {
        Phase warPhase = targetWar.value; 
        List<WarTile> tiles = new List<WarTile>();

        foreach(Theater theater in warPhase.GetComponentsInChildren<Theater>())
        {
            tiles.AddRange(theater.warTiles[targetFaction]); 
        }

        // TODO Not handling War Tiles really yet
    }
}
