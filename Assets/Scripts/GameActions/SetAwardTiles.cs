using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class SetAwardTiles : GameAction
    {
        protected override void Do()
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
            {
                Queue<AwardTile> awardTiles = new(Game.AwardTiles);

                foreach (Map map in Game.Spaces.Select(space => space.map).Distinct().OrderBy(tile => Random.value))
                    commands.Add(new SetAwardTileCommand(map, awardTiles.Dequeue()));
            }
        }
    }
}