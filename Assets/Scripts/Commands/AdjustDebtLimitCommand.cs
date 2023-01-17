using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class AdjustDebtLimitCommand : Command
    {
        [SerializeField] Faction faction;
        [SerializeField] int amount;

        public AdjustDebtLimitCommand(Faction faction, int amount)
        {
            this.faction = faction;
            this.amount = amount;
        }

        public override void Do(IAction action)
        {
            int debtLimit = Mathf.Max(0, RecordsTrack.debtLimit[faction] + amount);
            Debug.Log($"Adjusting {faction} Debt Limit by {amount} (to {debtLimit})");

            RecordsTrack.debtLimit[faction] = debtLimit;
            RecordsTrack.adjustDebtLimitEvent.Invoke();
        }
    }
}