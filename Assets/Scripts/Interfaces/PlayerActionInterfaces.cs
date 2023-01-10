using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public interface IPlayerAction
    {
        public string Name { get; }
        public Player Player { get; }
        public bool Passive { get; }
        public bool Can();
        public Task Execute(); 
        public Stack<Command> Commands { get; }
    }

    public interface PurchaseAction : IPlayerAction
    {
        public ActionPoint ActionCost { get; }
    }

    public interface TargetSpaceAction<T> : IPlayerAction
    {
        public T Space { get; }
        public void SetSpace(T space);
    }

    public interface RegionalPurchase<T> : PurchaseAction, TargetSpaceAction<T> where T : Space
    {
    }
}
