using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class CharlesHanburyWilliamsAction : MinisterAction
    {
        [SerializeField] List<PoliticalData> eligibleSpaces;

        protected override Task Do(IAction context)
        {
            // Add Effect to 
            return Task.CompletedTask;
        }
    }
}
