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
            foreach (Player p in Player.Players)
                foreach (Theater theater in nextWar.theaters)
                    Commands.Push(new AddWarTileToTheaterCommand(p.WarTiles.OrderBy(x => Random.value).FirstOrDefault(), theater));

            return Task.CompletedTask; 
        }
    }
}