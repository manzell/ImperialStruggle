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
                IEnumerable<Space> eligible = Game.Spaces.Where(space =>
                    !space.conflictMarker && space.control == Player.Opponent.Faction && space.map == theater.map &&
                    (space is Market || space is PoliticalSpace)); 

                Space space = (await new Selection<Space>(Player, eligible).Completion).First();

                throw new System.NotImplementedException(); 
            }
        }
    }
}
