using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class KeywordCondition : Conditional<Player>
    {
        [SerializeField] MinistryCard.Keyword keyword;

        protected override bool Test(Player player) => player.Keywords.Contains(keyword);
    }
}