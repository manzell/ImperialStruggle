using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class KeywordsCalc : Calculation<int>
    {
        [SerializeField] MinistryCard.Keyword keyword;
        protected override int Calc(IAction context) => (context as PlayerAction).Player.Ministers.Count(minister => minister.data.keywords.Contains(keyword));
    }
}
