using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class DeploySquadronAction : PlayerAction, PurchaseAction, TargetSpaceAction
    {
        NavalSpace space;
        Squadron squadron;
        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionType.Military, ActionPoint.ActionTier.Minor,
            space.Squadron == null ? 1 : squadron.space == null ? 3 : 2);

        public Space Space => space;
        public override bool Can() => space != null && base.Can();
        public void SetSpace(Space space) => this.space = space is NavalSpace ? (NavalSpace)space : null;

        protected override Task Do()
        {
            Commands.Push(new DeploySquadronCommand(squadron, space));
            return Task.CompletedTask; 
        }
    }
}