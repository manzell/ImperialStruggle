using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;

namespace ImperialStruggle
{
    public class DealCardCommand : Command
    {
        public static System.Action<Player, EventCard> dealCardEvent;
        [SerializeField] Player player;

        Stack<EventCard> dealtCards = new();

        public DealCardCommand(Player player)
        {
            this.player = player;
        }

        public override void Do(GameAction action)
        {
            EventCard card = Game.EventDeck.Pop();

            player.hand.Add(card);

            dealtCards.Push(card);
            dealCardEvent?.Invoke(player, card);

            Debug.Log($"{card.Name} dealt to {player.name}");
        }

        public override void Undo()
        {
            foreach (EventCard card in dealtCards)
            {
                player.hand.Remove(card);
                Game.EventDeck.Push(card);
            }
        }
    }
}