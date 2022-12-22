using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public class RemoveConflictMarkerAction : PlayerAction, PurchaseAction, TargetSpaceAction
    {
        public Space Space { get; private set; }
        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Military, 
            Space is Market market && market.Protected ? 1 : 2);

        public override bool Can() => Space != null && base.Can() && Space.Flag == Player.Faction && Eligible(Space);
        public void SetSpace(Space space) => Space = space;

        public override bool Eligible(Space space) => space != null & space.conflictMarker;

        protected override Task Do()
        {
            Commands.Push(new RemoveConflictMarkerCommand(Space));
            return Task.CompletedTask; 
        }
    }
}