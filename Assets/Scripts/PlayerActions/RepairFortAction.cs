using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

namespace ImperialStruggle
{
    public class RepairFortAction : PlayerAction, PurchaseAction, TargetSpaceAction
    {
        Fort space;
        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionType.Military, ActionPoint.ActionTier.Minor,
            space.FlagCost + (space.Flag == Player.Faction ? -1 : 0) + (space.Flag == Player.Opponent.Faction ? 1 : 0));

        public Space Space => space; 

        public override bool Can() => base.Can() && space != null && space.damaged == true && 
            (space.Flag != Player.Opponent.Faction || space.adjacentSpaces.Where(space => space.control == Player.Faction).Any(space => space is Market || space is NavalSpace));

        public void SetSpace(Space space) => this.space = space is Fort ? (Fort)space : null;

        protected override Task Do()
        {
            Commands.Push(new RemoveDamageMarkerCommand(space));

            if (space.Flag != Player.Faction)
                Commands.Push(new FlagSpaceCommand(space, Player.Faction));

            return Task.CompletedTask; 
        }
    }
}