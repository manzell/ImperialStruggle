using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class RobertWalpoleAction : MinisterAction
    {
        protected override async Task Do()
        {
            Exhausted = true;
            Commands.Push(new DealCardCommand(Player));

            Debug.LogWarning("Selection does not explicitly have a one-option minimum set"); 
            Selection<EventCard> selection = new(Player, Player.Cards, Finish);
            await selection.Completion; 
        }

        void Finish(Selection<EventCard> selection)
        {
            Commands.Push(new DiscardCardCommand(Player, selection.First()));
        }
    }
}
