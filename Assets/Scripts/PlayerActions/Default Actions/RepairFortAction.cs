using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

namespace ImperialStruggle
{
    public class RepairFortAction : PlayerAction, _PurchaseAction, TargetSpaceAction<Fort>
    {
        public ActionPoint ActionCost
        {
            get
            {
                ActionPoint AP = Space.flagCost.GetAPCost(Player, Space);
                AP.AdjustBaseValue((Space.Flag == Player.Opponent.Faction ? 1 : 0) - (Space.Flag == Player.Faction ? 1 : 0)); 

                return AP; 
            }
        }

        public RepairFortAction()
        {
            Name = "Repair Fort";
        }

        public Fort Space {get; private set;}

        public override bool Can(Player player) => Space != null && base.Can(player) && Space.damaged == true && 
            (Space.Flag != Player.Opponent.Faction || Space.adjacentSpaces.Where(space => space.Control == Player.Faction).Any((object space) => space is Market || space is NavalSpace));

        public void SetSpace(Fort space) => Space = space;
        public override bool Eligible(Space space) => space is Fort fort && fort.damaged; 

        protected override Task Do(IAction context)
        {
            Commands.Push(new RemoveDamageMarkerCommand(Space));
            Commands.Push(new FlagSpaceCommand(Space, Player.Faction));

            return Task.CompletedTask;
        }
    }
}