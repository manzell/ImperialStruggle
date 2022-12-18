using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using Sirenix.OdinInspector;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public abstract class GameAction
    {
        public List<Conditional> conditionals { get; private set; }
        public Stack<Command> Commands { get; private set; }

        public GameAction()
        {
            Commands = new();
            conditionals = new(); 
        }

        public void Queue(Command command) => Commands.Push(command); 

        public async Task Execute()
        {
            if (Can())
            {
                if (Commands == null)
                    Commands = new();

                await Do(); // Execution should pause here
                Phase.CurrentPhase.Push(this); // Command execution happens here. 
            }
        }

        public virtual bool Can()
        {
            if (conditionals == null)
                conditionals = new(); 
            bool retVal = conditionals.All(c => c.Test(this));
            return retVal;
        }

        protected abstract Task Do();
    }
}