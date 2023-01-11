using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class DeploySquadronAction : PlayerAction, PurchaseAction, TargetSpaceAction<NavalSpace>
    {
        Squadron squadron;
        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Military,
            Space.Squadron == null ? 1 : squadron.space == null ? 3 : 2);

        public NavalSpace Space { get; private set; }
        public override bool Can() => Space != null && base.Can();
        public void SetSpace(NavalSpace space) => Space = space; 

        public override bool Eligible(Space space) => space is NavalSpace;

        protected override Task Do()
        {
            Commands.Push(new DeploySquadronCommand(squadron, Space));
            return Task.CompletedTask; 
        }

        public override void Setup(Player player)
        {
            Name = "Deploy Squadron"; 
        }
    }
}