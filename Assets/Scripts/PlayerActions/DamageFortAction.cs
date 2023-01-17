using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class DamageFortAction : PlayerAction
    {
        protected override Task Do(IAction context)
        {
            return Task.CompletedTask; 

        }
    }
}
