using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class DiscardCardCommand : Command
    {
        Player player;
        EventCard card; 

        public DiscardCardCommand(Player player, EventCard card)
        {
            this.player = player;
            this.card = card; 
        }

        public override void Do(GameAction action)
        {
            player.Cards.Remove(card);
        }
    }
}
