using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.Utilities;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class SelectMinistryCardAction : GameAction
    {
        protected override async Task Do()
        {
            List<Task> tasks = new(); 

            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
            {
                foreach (Player player in Player.Players)
                {
                    Selection<MinistryCardData> selection = new(player, player.Ministers.Keys.Where(minister => minister.eras.Contains(peaceTurn.era)),
                        selection => selection.ForEach(minister => Commands.Push(new SelectMinistryCardCommand(player, minister))), 2);

                    selection.SetTitle("Select your Ministry Card(s)");

                    tasks.Add(selection.Completion); 
                }

                await Task.WhenAll(tasks);
            }
        }
    }
}