using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class CharlesHanburyWilliamsAction : MinisterAction
    {
        [SerializeField] List<PoliticalData> eligibleSpaces;

        public override Task Do(Player player)
        {
            // Add Effect to 
            return Task.CompletedTask;
        }
    }
}
