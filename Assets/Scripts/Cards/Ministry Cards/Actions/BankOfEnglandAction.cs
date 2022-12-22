using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class BankOfEnglandDebtLimitAction : MinisterAction
    {
        public override Task Do(Player player)
        {
            Exhausted = true;
            Commands.Push(new AdjustDebtLimitCommand(player.Faction, 1));
            return Task.CompletedTask; 
        }
    }

    public class BankOfEnglandEconEventAction : MinisterAction
    {
        protected override bool Can(Player player) => base.Can() && Phase.CurrentPhase is ActionRound ar && 
            ar.investmentTile.actions.Any(action => action is PlayEventCardAction) && player.Cards.Count() > 0;

        public override Task Do(Player player)
        {
            Exhausted = true;
            Commands.Push(new AdjustDebtLimitCommand(player.Faction, 1));
            return Task.CompletedTask;
        }
    }
}
