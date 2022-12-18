using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class KeywordCondition : Conditional
    {
        [SerializeField] MinistryCard.Keyword keyword;

        public override bool Test(GameAction action)
        {
            if (action is PlayerAction playerAction)
                return playerAction.Player.Keywords.Contains(keyword);
            else
                return true;
        }
    }
}