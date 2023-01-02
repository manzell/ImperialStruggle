using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class BankOfEnglandDebtLimitAction : MinisterAction
    {
        protected override Task Do()
        {
            Exhausted = true;
            Commands.Push(new AdjustDebtLimitCommand(Player.Faction, 1));
            return Task.CompletedTask; 
        }
    }

    public class BankOfEnglandEconEventAction : MinisterAction
    {
        protected override bool Can(Player player) => base.Can() && Phase.CurrentPhase is ActionRound ar && 
            ar.investmentTile.actions.Any(action => action is PlayEventCardAction) && player.Cards.Count() > 0;

        protected override Task Do()
        {
            Exhausted = true;
            Commands.Push(new AdjustDebtLimitCommand(Player.Faction, 1));
            return Task.CompletedTask;
        }
    }
}
