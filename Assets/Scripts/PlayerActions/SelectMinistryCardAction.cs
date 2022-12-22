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
                        Finish, 2); 

                    selection.SetTitle("Select your Ministry Card(s)");

                    tasks.Add(selection.Completion); 
                }

                await Task.WhenAll(tasks);
            }
        }

        void Finish(Selection<MinistryCardData> selection)
        {
            Debug.Log($"{selection.player} seleted {string.Join(" & ", selection.selectedItems.Select(m => m.Name))}"); 
            foreach(MinistryCardData minister in selection.selectedItems)
                Commands.Push(new SelectMinistryCardCommand(selection.player, minister));
        }
    }
}