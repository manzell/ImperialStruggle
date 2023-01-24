using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class AvailableDebtCondition : Conditional<PlayerAction>
    {
        enum ConditionType { Exactly, MoreThan, FewerThan, NotMoreThan, NotLessThan }
        [SerializeField] ConditionType conditionType;
        [SerializeField] int margin = 1;

        protected override bool Test(PlayerAction action)
        {
            int availableDebt = RecordsTrack.availableDebt[action.Player.Faction];

            switch (conditionType)
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