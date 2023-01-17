using Sirenix.OdinInspector.Editor.Drawers;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class InterestPayments_Base : PlayerAction
    {
        [SerializeField] int vpAward = 1;
        [SerializeField] int debtLimitReduction = 1; 
        protected override Task Do(IAction context)
        {
            Commands.Push(new AdjustDebtLimitCommand(Player.Opponent.Faction, -debtLimitReduction));

            if (RecordsTrack.availableDebt[Player.Opponent.Faction] > 0)
            {
                Commands.Push(new AdjustDebtCommand(Player.Opponent.Faction, -debtLimitReduction));
                Commands.Push(new AdjustVPCommand(Player.Faction, vpAward)); 
            }

            return Task.CompletedTask; 
        }
    }

    public class InterestPayments_Bonus : PlayerAction
    {
        [SerializeField] int debtReduction = 2; 

        protected override Task Do(IAction context)
        {
            Commands.Push(new AdjustDebtCommand(Player.Faction, -debtReduction));
            return Task.CompletedTask; 
        }
    }
}
