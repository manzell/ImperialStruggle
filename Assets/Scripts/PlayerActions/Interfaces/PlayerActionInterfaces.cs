using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public interface PurchaseAction
    {
        public ActionPoint ActionCost { get; }
    }

    public interface RegionalPurchase : PurchaseAction
    {
        public FlaggableSpace Space { get; }
    }

    public interface TargetSpaceAction
    {
        public Space Space { get; }
        public void SetSpace(Space space);
    }
}
