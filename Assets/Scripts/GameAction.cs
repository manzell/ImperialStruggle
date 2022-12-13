using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using Sirenix.OdinInspector;
using System.Linq;
using System.Runtime.CompilerServices;

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

        public void Execute()
        {
            if (Can())
            {
                if (Commands == null)
                    Commands = new();

                Do();
                Phase.CurrentPhase.Push(this);
            }
        }

        public virtual bool Can()
        {
            if (conditionals == null)
                conditionals = new(); 
            bool retVal = conditionals.All(c => c.Test(this));
            return retVal;
        }

        protected abstract void Do();
    }
}