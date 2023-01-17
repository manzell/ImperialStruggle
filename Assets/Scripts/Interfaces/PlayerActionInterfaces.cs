using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public interface IAction
    {
        public string Name { get; }
        public Stack<Command> Commands { get; }
        public Task Execute(IAction context = null);
    }

    public interface _PurchaseAction : IAction
    {
        public ActionPoint ActionCost { get; }
    }

    public interface TargetSpaceAction<T> : IAction
    {
        public T Space { get; }
        public void SetSpace(T space);
    }

    public interface RegionalPurchase<T> : _PurchaseAction, TargetSpaceAction<T> where T : Space
    {
    }

    public interface PassiveAction : IAction { }
}
