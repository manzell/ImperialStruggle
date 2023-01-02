using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public interface IPlayerAction
    {
        public string Name { get; }
        public bool Can();
        public Task Execute(); 
        public Stack<Command> Commands { get; }
    }

    public interface PurchaseAction : IPlayerAction
    {
        public Player Player { get; }
        public ActionPoint ActionCost { get; }
    }

    public interface RegionalPurchase : PurchaseAction
    {
        public FlaggableSpace Space { get; }
    }

    public interface TargetSpaceAction : IPlayerAction
    {
        public Space Space { get; }
        public void SetSpace(Space space);
    }
}
