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
        public static System.Action<MinistryCardData> SelectEvent;

        public SelectMinistryCardCommand(MinistryCardData card) => this.card = card;

        public override void Do(GameAction action)
        {
            Player player = Player.players.Where(player => player.faction == card.faction).First();

            player.ministers[card] = MinistryCard.MinistryCardStatus.Selected;
            SelectEvent?.Invoke(card);
        }

        public override void Undo()
        {
            Player player = Player.players.Where(player => player.faction == card.faction).First();
            player.ministers[card] = MinistryCard.MinistryCardStatus.Reserved;
        }
    }
}