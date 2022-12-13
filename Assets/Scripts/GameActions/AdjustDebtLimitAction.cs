using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ImperialStruggle
{
    public class AdjustDebtLimitAction : GameAction
    {
        public int amount;

        protected override void Do()
        {
            foreach (Faction faction in RecordsTrack.debtLimit.Keys)
                Queue(new AdjustDebtCommand(faction, amount));
        }
    }
}