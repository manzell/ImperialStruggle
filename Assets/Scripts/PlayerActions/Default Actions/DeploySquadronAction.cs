using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class DeploySquadronAction : PlayerAction, _PurchaseAction, TargetSpaceAction<NavalSpace>
    {
        Squadron squadron;
        public NavalSpace Space { get; private set; }
        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Military,
            Space.Squadron == null ? 1 : squadron.space == null ? 3 : 2);

        public DeploySquadronAction()
        {
            Name = "Deploy Squadron"; 
        }

        public override bool Can(Player player) => Space != null && base.Can(player);
        public void SetSpace(NavalSpace space) => Space = space; 


        public override bool Eligible(Space space) => space is NavalSpace;

        protected override Task Do(IAction context)
        {
            Commands.Push(new DeploySquadronCommand(squadron, Space));
            return Task.CompletedTask; 
        }
    }
}