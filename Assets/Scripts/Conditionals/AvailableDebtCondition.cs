using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class AvailableDebtCondition : Conditional
    {
        [SerializeField] int margin = 1;

        public override bool Test(GameAction action)
        {
            /*
            if (action is ActionTarget<Faction> factionAction)
            {
                int availableDebt = RecordsTrack.availableDebt[factionAction.target];

                switch (ConditionalType)
                {
                    case Conditional.ConditionType.Exactly:
                        return availableDebt == margin;
                    case Conditional.ConditionType.NotLessThan:
                        return availableDebt >= margin;
                    case Conditional.ConditionType.NotMoreThan:
                        return availableDebt <= margin;
                    default:
                        return true;
                }
            }
            */
            return false;
        }
    }
}