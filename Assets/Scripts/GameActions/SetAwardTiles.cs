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
            Queue<AwardTile> awardTiles = new(Game.AwardTiles);

            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
                foreach (Map map in Game.Spaces.Select(space => space.map).Distinct())
                    Commands.Push(new SetAwardTileCommand(map, awardTiles.Dequeue()));
        }
    }
}