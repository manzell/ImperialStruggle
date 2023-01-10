using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class PlayerActionCondition : Conditional
    {
        [SerializeField] List<PlayerAction> eligibleActions = new(); 

        public override bool Test(IPlayerAction context) => eligibleActions.Any(action => action.GetType() == context.GetType()); 
    }
}
