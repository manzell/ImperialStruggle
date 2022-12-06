using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class WarPrepAction : GameAction
{
    [SerializeField] WarTurn nextWar; 

    protected override void Do()
    {
        foreach(Player player in Player.players)
            foreach(Theater theater in nextWar.theaters)
                commands.Push(new AddWarTileToTheaterCommand(player.warTiles.Dequeue(), theater)); 
    }
}