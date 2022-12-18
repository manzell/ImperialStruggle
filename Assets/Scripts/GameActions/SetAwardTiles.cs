using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public class SetAwardTiles : GameAction
    {
        protected override Task Do()
        {
            Queue<AwardTile> awardTiles = new(Game.AwardTiles);

            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
                foreach (Map map in Game.Spaces.Select(space => space.map).Distinct())
                    Commands.Push(new SetAwardTileCommand(map, awardTiles.Dequeue()));

            return Task.CompletedTask;
        }
    }
}