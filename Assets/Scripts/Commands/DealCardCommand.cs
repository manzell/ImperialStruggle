using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;

namespace ImperialStruggle
{
    public class DealCardCommand : Command
    {
        public static System.Action<Player, EventCard> DealCardEvent;
        [SerializeField] Player player;

        Stack<EventCard> dealtCards = new();

        public DealCardCommand(Player player)
        {
            this.player = player;
        }

        public override void Do(IAction action)
        {
            EventCard card = Game.EventDeck.Pop();

            player.Cards.Add(card);

            dealtCards.Push(card);
            DealCardEvent?.Invoke(player, card);

            Debug.Log($"{card.Name} dealt to {player.name}");
        }

        public override void Undo()
        {
            foreach (EventCard card in dealtCards)
            {
                player.Cards.Remove(card);
                Game.EventDeck.Push(card);
            }
        }
    }
}