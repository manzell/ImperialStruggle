using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public class RemoveConflictMarkerAction : PlayerAction, PurchaseAction, TargetSpaceAction
    {
        public Space Space { get; private set; }
        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionType.Military, ActionPoint.ActionTier.Minor, 
            Space is Market market && market.Protected ? 1 : 2);

        public override bool Can() => Space != null && base.Can() && Space.Flag == Player.Faction && Space.conflictMarker;

        public void SetSpace(Space space) => this.Space = space;

        protected override Task Do()
        {
            Commands.Push(new RemoveConflictMarkerCommand(Space));
            return Task.CompletedTask; 
        }
    }
}