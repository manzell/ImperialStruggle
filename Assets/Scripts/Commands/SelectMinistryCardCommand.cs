using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class SelectMinistryCardCommand : Command
    {
        MinistryCardData card;
        Player player; 
        public static System.Action<MinistryCardData> SelectEvent;

        public SelectMinistryCardCommand(Player player, MinistryCardData card)
        {
            this.card = card;
            this.player = player;
        }

        public override void Do(GameAction action)
        {
            player.Ministers[card] = MinistryCard.MinistryCardStatus.Selected;
            SelectEvent?.Invoke(card);
        }

        public override void Undo() => player.Ministers[card] = MinistryCard.MinistryCardStatus.Reserved;
    }
}