using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class RobertWalpoleAction : MinisterAction
    {
        public override async Task Do(Player player)
        {
            Exhausted = true;
            Commands.Push(new DealCardCommand(player));

            Selection<EventCard> selection = new(player, player.Cards, Finish);
            await selection.Completion; 
        }

        void Finish(Selection<EventCard> selection)
        {
            Commands.Push(new DiscardCardCommand(Player, selection.First())); 
        }
    }
}
