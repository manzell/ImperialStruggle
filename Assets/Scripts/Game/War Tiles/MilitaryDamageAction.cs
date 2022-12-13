using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class MilitaryDamageAction : PlayerAction
    {
        protected override void Do()
        {
            if(Phase.CurrentPhase is Theater theater)
            {
                List<Space> eligible = Game.Spaces.Where(space =>
                    ((space is Fort fort && fort.damaged == false) || space is NavalSpace) && space.control == player.Opponent.faction &&
                    space.map == theater.map).ToList();

                new Selection<Space>(player, eligible, Finish);
            }

            void Finish(Selection<Space> selection)
            {
                if (selection.First() is Fort fort)
                    Commands.Push(new DamageFortCommand(fort));
                else if (selection.First() is NavalSpace navalSpace)
                    Commands.Push(new ReturnFleetToNavalBoxCommand(navalSpace));
            }
        }
    }
}
