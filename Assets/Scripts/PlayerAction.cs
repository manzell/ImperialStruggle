using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public abstract class PlayerAction : ISelectable, IAction
    {
        public string Name { get; protected set; }
        public Stack<Command> Commands { get; private set; } // TODO Make this Fully Private and force usage of the Queue() Method. 
        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }
        public Player Player { get; private set; }

        public virtual bool Eligible(Space space) => false;

        public virtual bool Can(Player player)
        {
            bool retVal = true;// = CanFilters.All(can => can.Calculate(this));

            if (this is _PurchaseAction purchaseAction)
                retVal &= player.ActionPoints.Can(purchaseAction);

            return retVal;
        }

        protected virtual Task Do(IAction context) => Task.CompletedTask;

        public async virtual Task Execute(IAction context = null)
        {
            Player = Player ?? (context as PlayerAction)?.Player ?? (Phase.CurrentPhase as ActionRound)?.player ?? Game.ActivePlayer; 

            if (Can(Player))
            {
                Commands = new();

                await Do(context); // Execution should pause here

                if (this is _PurchaseAction purchaseAction)
                    Player.ActionPoints.Charge(purchaseAction);

                Phase.CurrentPhase.Push(this); // Command execution happens here. 
            }
        }

        public void SetPlayer(Player player) => Player = player; 
    }
}