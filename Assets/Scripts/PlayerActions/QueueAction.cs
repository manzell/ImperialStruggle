using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class QueueAction : PlayerAction
    {
        [SerializeField] List<Command> commands = new();
        [SerializeField] List<Conditional> conditions = new(); 

        protected override Task Do()
        {
            if(conditionals.All(condition => condition.Test(this)))
                commands.ForEach(command => Commands.Push(command));

            return Task.CompletedTask; 
        }
    }
}
