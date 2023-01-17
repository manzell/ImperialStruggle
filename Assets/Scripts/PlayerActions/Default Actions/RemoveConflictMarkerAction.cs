using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public class RemoveConflictMarkerAction : PlayerAction, _PurchaseAction, TargetSpaceAction<Space>
    {
        public Space Space { get; private set; }
        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Military, 
            Space is Market market && market.Protected ? 1 : 2);

        public override bool Can(Player player) => Space != null && base.Can(player) && Space.Flag == player.Faction && Eligible(Space);
        public void SetSpace(Space space) => Space = space;

        public override bool Eligible(Space space) => space != null & space.ConflictMarkers.Count > 0;

        protected override Task Do(IAction context)
        {
            Commands.Push(new RemoveConflictMarkerCommand(Space));
            return Task.CompletedTask; 
        }
    }
}