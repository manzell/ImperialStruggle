using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class SelectMinistryCardCommand : Command
    {
        MinistryCard card;
        Player player; 
        public static System.Action<MinistryCard> SelectEvent;

        public SelectMinistryCardCommand(Player player, MinistryCardData data)
        {
            this.player = player;
            card = new MinistryCard(data);
        }

        public override void Do(GameAction action)
        {
            player.Ministers.Add(card); 
            card.ministryCardStatus = MinistryCard.MinistryCardStatus.Selected;
            SelectEvent?.Invoke(card);
        }
    }
}