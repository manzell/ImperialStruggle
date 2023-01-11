using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class WarUnflagAction : PlayerAction
    {
        protected async override Task Do()
        {
            if (Phase.CurrentPhase is Theater theater)
            {
                IEnumerable<FlaggableSpace> eligible = Game.Spaces.Where(space =>
                    space.ConflictMarkers.Count == 0 && space.Control == Player.Opponent.Faction && space.map == theater.map &&
                    (space is Market || space is PoliticalSpace)).Select(space => space as FlaggableSpace); 

                Commands.Push(new UnflagCommand((await new Selection<FlaggableSpace>(Player, eligible).Completion).First())); 
            }
        }
    }
}
