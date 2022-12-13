using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEditor;

namespace ImperialStruggle
{
    public class SetInitiativeAction : PlayerAction
    {
        protected override void Do()
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
            {
                Player initiativePlayer = Player.players.Where(player => player.faction == peaceTurn.initiative).First();
                Selection<Faction> selection = new(initiativePlayer, new List<Faction>() { Game.Britain, Game.France }, Finish);
                //selection.SetTitle($"{actingPlayer.faction} selects Initiative");
            }
        }

        void Finish(IEnumerable<Faction> factions)
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
            {
                ActionRound[] actionRounds = Phase.CurrentPhase.GetComponentsInChildren<ActionRound>();
                peaceTurn.initiative = factions.First();

                Debug.Log($"{player.faction} elects to {(player == peaceTurn.initiative ? "Play" : "Pass")} the first Action Round; " +
                    $"{player.faction.Opposition()} will go {(player.faction.Opposition() == peaceTurn.initiative ? "First" : "Second")}");

                Debug.Log("Use a command to change the initiative!");

                for (int i = 0; i < actionRounds.Length; i++)
                    actionRounds[i].actingFaction = i % 2 == 0 ? peaceTurn.initiative : peaceTurn.initiative.Opposition();
            }
        }
    }
}