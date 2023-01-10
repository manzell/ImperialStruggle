using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    // This is an active action
    public class BankOfEnglandDebtLimitAction : MinisterAction
    {
        protected override Task Do()
        {
            Exhausted = true;
            Commands.Push(new AdjustDebtLimitCommand(Player.Faction, 1));
            return Task.CompletedTask; 
        }
    }

    // So this is a passive action
    public class BankOfEnglandEconEventAction : MinisterAction
    {
        public override bool Can() => base.Can() && Phase.CurrentPhase is ActionRound ar && 
            ar.investmentTile.actions.Any(action => action is PlayEventCardAction) && Player.Cards.Count() > 0;

        public override void Reveal()
        {
            PlayEventCardAction.SelectEventCardEvent += AddDiplomaticCards;
        }

        // This is a Passive Action which doesn't actually need a Do. Should it be part of the Action System at all?
        protected override Task Do() { return Task.CompletedTask; }

        void AddDiplomaticCards(Selection<EventCard> selection)
        {
            Debug.Log("Add Diplomatic Card");

            if(!Exhausted)
                foreach (EventCard card in Player.Cards.Where(card => card.reqdActionType == ActionPoint.ActionType.Diplomacy && !selection.selectableItems.Contains(card)))
                    selection.Add(card); 
        }
    }
}
