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

        public void SetSpace(Space space) => this.space = space is PoliticalSpace ? (PoliticalSpace)space : null;

        public ActionPoint ActionCost => new (space.Flag == Player.Opponent.Faction ? ActionPoint.ActionTier.Major : ActionPoint.ActionTier.Minor,
            ActionPoint.ActionType.Diplomacy, space.GetFlagCost(Player));

        public override bool Can() => space == null ? false : base.Can();
        public override bool Eligible(Space space) => space is PoliticalSpace;

        protected override Task Do()
        {
            if (space.Flag == null)
                Commands.Push(new FlagSpaceCommand(space, Player.Faction));
            if (space.Flag == Player.Opponent.Faction)
                Commands.Push(new UnflagCommand(space));

            return Task.CompletedTask;
        }

        public override void Setup(Player player)
        {
            Name = "Shift Space";
            base.Setup(player);
        }
    }
}
