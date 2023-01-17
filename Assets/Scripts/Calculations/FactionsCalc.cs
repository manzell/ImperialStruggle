using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class FactionsCalc : Calculation<IEnumerable<ISelectable>>
    {
        protected override IEnumerable<ISelectable> Calc(IAction context) => Player.Players.Select(player => player.Faction); 
    }

    public class InitiativeFactionCalc : Calculation<Faction>
    {
        protected override Faction Calc(IAction context)
        {
            if (RecordsTrack.VictoryPoints > 15)
                return Game.Britain;
            else if (RecordsTrack.VictoryPoints < 15)
                return Game.France;
            else
            {
                PeaceTurn peaceTurn = Phase.CurrentPhase as PeaceTurn;
                int i = peaceTurn.transform.GetSiblingIndex();

                return peaceTurn.initiative.Faction ??
                    peaceTurn.transform.parent.GetComponentsInChildren<PeaceTurn>().Where(pt => pt.transform.GetSiblingIndex() < i).Last().initiative.Faction ??
                    Game.France;
            }
        }
    }
}
