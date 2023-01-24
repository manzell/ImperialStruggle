using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class KeywordCondition : Conditional<PlayerAction>
    {
        [SerializeField] MinistryCard.Keyword keyword;

        protected override bool Test(PlayerAction action) => action.Player.Keywords.Contains(keyword);
    }
}