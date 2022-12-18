using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEditor;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class SetInitiativeAction : GameAction
    {
        protected override async Task Do()
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
            {
                Player initiativePlayer = GetInitiativePlayer(peaceTurn); 
                Selection<Player> selection = new (initiativePlayer, Player.Players, Finish);
                selection.SetTitle($"{initiativePlayer.Faction.Name} selects which faction will act first:");

                await selection.Completion; 

                void Finish(IEnumerable<Player> players)
                {
                    Player player = players.First(); 
                    Debug.Log($"{initiativePlayer.Faction.name} elects to {(player == peaceTurn.initiative ? "Play" : "Pass")} the first Action Round; " +
                            $"{player.Faction.Opposition().Name} will go {(player.Faction.Opposition() == peaceTurn.initiative ? "First" : "Second")}");

                    ActionRound[] actionRounds = Phase.CurrentPhase.GetComponentsInChildren<ActionRound>();

                    for (int i = 0; i < actionRounds.Length; i++)
                        actionRounds[i].Setup(i % 2 == 0 ? peaceTurn.initiative : peaceTurn.initiative.Opponent);
                }
            }
        }

        Player GetInitiativePlayer(PeaceTurn peaceTurn)
        {
            if (RecordsTrack.VictoryPoints > 15)
                return Player.Players.Where(player => player.Faction == Game.Britain).First();
            else if (RecordsTrack.VictoryPoints < 15)
                return Player.Players.Where(player => player.Faction == Game.France).First();
            else
            {
                int l = peaceTurn.transform.GetSiblingIndex();

                return peaceTurn.initiative ??
                    peaceTurn.transform.parent.GetComponentsInChildren<PeaceTurn>().Where(pt => pt.transform.GetSiblingIndex() < l).Last().initiative ??
                    Player.Players.Where(player => player.Faction == Game.France).First();
            }
        }
    }
}