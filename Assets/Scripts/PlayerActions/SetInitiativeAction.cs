using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class SetInitiativeAction : GameAction
    {
        protected override async Task Do()
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
            {
                Player initiativePlayer = new InitiativeFactionCalc().Calculate(this).player; 

                Selection<Player> selection = new (initiativePlayer, Player.Players, Finish);
                selection.SetTitle($"{initiativePlayer.Faction.Name} selects which faction will act first:");

                await selection.Completion; 

                void Finish(Selection<Player> selection)
                {
                    Faction faction = selection.First().Faction;
                    Debug.Log($"{faction.name} elects to {(faction == peaceTurn.initiative ? "Play" : "Pass")} the first Action Round; " +
                            $"{faction.Opposition().Name} will go {(faction.Opposition() == peaceTurn?.initiative ? "First" : "Second")}");

                    ActionRound[] actionRounds = Phase.CurrentPhase.GetComponentsInChildren<ActionRound>();

                    for (int i = 0; i < actionRounds.Length; i++)
                        actionRounds[i].Setup(i % 2 == 0 ? peaceTurn.initiative : peaceTurn.initiative.Opponent);
                }
            }
        }
    }
}