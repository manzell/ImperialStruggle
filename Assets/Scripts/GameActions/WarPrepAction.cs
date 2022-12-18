using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class WarPrepAction : GameAction
    {
        [SerializeField] WarTurn nextWar;

        protected override Task Do()
        {
            foreach (Player player in Player.Players)
                foreach (Theater theater in nextWar.theaters)
                    Commands.Push(new AddWarTileToTheaterCommand(player.WarTiles.Dequeue(), theater));

            return Task.CompletedTask; 
        }
    }
}