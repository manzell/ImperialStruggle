using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class PlayerActionCondition : Conditional<IAction>
    {
        [SerializeField] List<PlayerAction> eligibleActions = new(); 

        protected override bool Test(IAction context) => eligibleActions.Any(action => action.GetType() == context.GetType()); 
    }
}
