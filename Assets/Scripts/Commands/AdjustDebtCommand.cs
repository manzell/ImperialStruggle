using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class AdjustDebtCommand : Command
    {
        public Faction faction;
        public int amount;

        public AdjustDebtCommand(Faction faction, int amount)
        {
            this.faction = faction;
            this.amount = amount;
        }

        public override void Do(GameAction action)
        {
            if (amount != 0 && faction != null)
            {
                RecordsTrack.currentDebt[faction ?? (action as PlayerAction)?.Player.Faction] += amount;
                RecordsTrack.adjustDebtEvent.Invoke();
                // Check to see if Debt > Debt Limit
            }
        }
    }
}