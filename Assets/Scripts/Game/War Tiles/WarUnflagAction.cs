using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;

namespace ImperialStruggle
{
    public class WarUnflagAction : PlayerAction
    {
        protected override void Do()
        {
            if (Phase.CurrentPhase is Theater theater)
            {
                List<Space> eligible = Game.Spaces.Where(space =>
                    ((space is Market market && !market.conflictMarker) || (space is PoliticalSpace polySpace && !polySpace.conflictMarker)) && 
                    space.control == player.Opponent.faction && space.map == theater.map).ToList();

                new Selection<Space>(player, eligible, selection => Commands.Push(new UnflagCommand(selection.First())));
            }
        }
    }
}
