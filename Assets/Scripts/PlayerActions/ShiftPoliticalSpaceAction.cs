using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public class ShiftPoliticalSpaceAction : PlayerAction, RegionalPurchase, TargetSpaceAction
    {
        PoliticalSpace space;
        public FlaggableSpace Space => space;
        Space TargetSpaceAction.Space => space;

        public void SetSpace(Space space)
        {
            Debug.Log(space);
            Debug.Log(space is PoliticalSpace); 
            this.space = space is PoliticalSpace ? (PoliticalSpace)space : null;
        }

        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionType.Diplomacy,
            space.Flag == Player.Opponent.Faction ? ActionPoint.ActionTier.Major : ActionPoint.ActionTier.Minor, space.FlagCost);

        public override bool Can()
        {
            if (space == null)
            {
                Debug.Log("Space not set");
                return false;
            }
            else
                return base.Can();
        }

        protected override Task Do()
        {
            if (space.Flag == null)
                Commands.Push(new FlagSpaceCommand(space, Player.Faction));
            if (space.Flag == Player.Opponent.Faction)
                Commands.Push(new UnflagCommand(space));

            return Task.CompletedTask;
        }
    }
}
