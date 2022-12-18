using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace ImperialStruggle
{
    public class BuildFortAction : PlayerAction, PurchaseAction, TargetSpaceAction
    {
        Fort space;
        IEnumerable<Fort> eligibleForts;
        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionType.Military, ActionPoint.ActionTier.Minor, space.FlagCost);

        public Space Space => space;
        public void SetSpace(Space space) => this.space = space is Fort ? (Fort)space : null;

        public override bool Can() => space != null && base.Can() && eligibleForts.Contains(space);

        protected override Task Do()
        {
            Commands.Push(new FlagSpaceCommand(space, Player.Faction));
            return Task.CompletedTask; 
        }

        void SetEligibleForts()
        {
            eligibleForts = Game.Spaces.OfType<Fort>().Where(space => 
                space.adjacentSpaces.Any(_space => (_space is Market || _space is NavalSpace || _space is Territory) && 
                (space.control == Game.Neutral || (space.control == Player.Opponent.Faction && space.damaged))));
        }
    }
}