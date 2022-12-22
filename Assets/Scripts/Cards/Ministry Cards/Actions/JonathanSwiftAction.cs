using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class JonathanSwiftAction : MinisterAction
    {
        [SerializeField] HashSet<PoliticalSpace> IrelandSpaces, ScotlandSpaces;

        bool canFlagWithMinorAP => IrelandSpaces.Any(space => space.control == Game.France); 

        public override Task Do(Player player)
        {
            Debug.Log("System Not Implemented");
            return Task.CompletedTask; 
        }
    }
}
