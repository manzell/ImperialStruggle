using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public abstract class GameAction : ISelectable, IAction
    {
        public string Name { get; protected set; }
        public Stack<Command> Commands { get; private set; } // TODO Make this Fully Private and force usage of the Queue() Method. 
        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }

        public GameAction()
        {
            Commands = new();
        }

        public void Queue(Command command) => Commands.Push(command); 

        public virtual async Task Execute(IAction context)
        {
            if (Can())
            {
                if (Commands == null)
                    Commands = new();

                await Do(); // Execution should pause here

                Phase.CurrentPhase.Push(this); // Command execution happens here. 
            }
        }

        public virtual bool Can() => true; 

        protected virtual Task Do() => Task.CompletedTask; // TODO - I probably need to pass in an Invoking Game Action here so I know what GameAction any Conditionals need to reference
    }
}