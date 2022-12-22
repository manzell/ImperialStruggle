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
        public string Name { get; protected set; }
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

                if (this is PurchaseAction purchaseAction)
                    purchaseAction.Player.ActionPoints.Charge(purchaseAction); 

                await Do(); // Execution should pause here
                Phase.CurrentPhase.Push(this); // Command execution happens here. 
            }
        }
        
        public virtual bool Can()
        {
            if (conditionals == null)
                conditionals = new(); 

            bool retVal = conditionals.All(c => c.Test(this));

            if (this is PurchaseAction purchaseAction)
                retVal &= purchaseAction.Player.ActionPoints.Can(purchaseAction);

            return retVal;
        }
        
        protected abstract Task Do();
    }
}