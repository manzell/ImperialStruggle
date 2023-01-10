using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class AvailableDebtCondition : Conditional
    {
        [SerializeField] int margin = 1;

        public override bool Test(IPlayerAction action)
        {
            int availableDebt = RecordsTrack.availableDebt[action.Player.Faction];

            switch (ConditionalType)
            {
                case ConditionType.Exactly:
                    return availableDebt == margin;
                case ConditionType.NotLessThan:
                    return availableDebt >= margin;
                case ConditionType.NotMoreThan:
                    return availableDebt <= margin;
                case ConditionType.MoreThan:
                    return availableDebt > margin;
                case ConditionType.FewerThan:
                    return availableDebt < margin;
                default:
                    return true;
            }
        }
    }
}