using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class AdjustTreatyPointsCommand : Command
    {
        Faction faction;
        int amount;

        public AdjustTreatyPointsCommand(Faction faction, int amount)
        {
            this.faction = faction;
            this.amount = amount;
        }

        public override void Do(GameAction action)
        {
            RecordsTrack.treatyPoints[faction] += amount;
            RecordsTrack.adjustTPEvent.Invoke();
        }
    }
}