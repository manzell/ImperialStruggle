using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class KeywordCondition : Conditional
    {
        [SerializeField] MinistryCard.Keyword keyword;

        public override bool Test(IPlayerAction action) => action.Player.Keywords.Contains(keyword);
    }
}