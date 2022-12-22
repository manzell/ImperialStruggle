using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class DeploySquadronAction : PlayerAction, PurchaseAction, TargetSpaceAction
    {
        NavalSpace navalSpace;
        Squadron squadron;
        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Military, 
            navalSpace.Squadron == null ? 1 : squadron.space == null ? 3 : 2);

        public Space Space => navalSpace;
        public override bool Can() => navalSpace != null && base.Can();
        public void SetSpace(Space space) => this.navalSpace = space is NavalSpace ? (NavalSpace)space : null;

        public override bool Eligible(Space space) => space is NavalSpace;

        protected override Task Do()
        {
            Commands.Push(new DeploySquadronCommand(squadron, navalSpace));
            return Task.CompletedTask; 
        }

        public override void Setup(Player player)
        {
            Name = "Deploy Squadron"; 
        }
    }
}