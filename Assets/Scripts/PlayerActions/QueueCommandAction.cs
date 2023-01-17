using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class QueueCommandAction : PlayerAction
    {
        [SerializeField] List<Command> commands;
        [SerializeField] List<Conditional<IAction>> conditionals;

        public void AddCommand(Command command) => commands.Append(command); 
        public void AddCondition(Conditional<IAction> condition) => conditionals.Append(condition);

        protected override Task Do(IAction context)
        {
            if(conditionals.All(condition => condition.Check(this)))
                commands.ForEach(command => Commands.Push(command)); 

            return Task.CompletedTask; 
        }
    }
}
