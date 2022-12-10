using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class KeywordCondition : Conditional
    {
        public MinistryCard.Keyword keyword;

        public Conditional.ConditionType ConditionalType => Conditional.ConditionType.Exactly;

        public string ConditionalText => "Keyword";

        // Checks to see if the ActivePlayer has the keyword on them
        public bool Test(GameAction action)
        {
            if (action is PlayerAction playerAction)
                return playerAction.actingPlayer.Keywords.Contains(keyword);
            else
                return true;
        }
    }
}