using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    // So this is a passive action TODO figure out another way
    public class BankOfEnglandEconEventAction : MinisterAction
    {
        List<EventCard> addedCards; 

        public override bool Can(Player player) => base.Can(player) && Phase.CurrentPhase is ActionRound ar && 
            ar.investmentTile.actions.Any(action => action is PlayEventCardAction) && player.Cards.Count() > 0;

        public override void Reveal(Player player)
        {
            PlayEventCardAction.SelectEventCardsEvent += AddDiplomaticCards;
            PlayEventCardAction.PlayEventCardEvent += CheckForAddedCard; 
        }

        protected override void Retire(Player player)
        {
            PlayEventCardAction.SelectEventCardsEvent -= AddDiplomaticCards;
            PlayEventCardAction.PlayEventCardEvent -= CheckForAddedCard;
        }

        void CheckForAddedCard(EventCard card)
        {
            if (addedCards.Contains(card))
                Exhausted = true; 
        }

        void AddDiplomaticCards(Selection<EventCard> selection)
        {
            foreach (EventCard card in Player.Cards.Where(card => card.reqdActionType == ActionPoint.ActionType.Diplomacy && !selection.selectableItems.Contains(card)))
            {
                addedCards.Add(card);
                selection.Add(card);
            }
        }
    }
}
