using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class MilitaryDamageAction : PlayerAction
    {
        protected async override Task Do()
        {
            if(Phase.CurrentPhase is Theater theater)
            {
                List<Space> eligible = Game.Spaces.Where(space =>
                    ((space is Fort fort && fort.damaged == false) || space is NavalSpace) && space.Control == Player.Opponent.Faction &&
                    space.map == theater.map).ToList();

                Selection<Space> selection = new Selection<Space>(Player, eligible);
                await selection.Completion;

                if (selection.First() is Fort fort)
                    Commands.Push(new DamageFortCommand(fort));
                else if (selection.First() is NavalSpace navalSpace)
                    Commands.Push(new ReturnFleetToNavalBoxCommand(navalSpace));
            }
        }
    }
}
