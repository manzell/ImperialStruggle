using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

namespace ImperialStruggle
{
    public class RepairFortAction : PlayerAction, PurchaseAction, TargetSpaceAction
    {
        Fort fort;
        public ActionPoint ActionCost
        {
            get
            {
                ActionPoint AP = fort.flagCost.GetAPCost(Player, fort);
                AP.AdjustBaseValue((fort.Flag == Player.Opponent.Faction ? 1 : 0) - (fort.Flag == Player.Faction ? 1 : 0)); 

                return AP; 
            }
        }

        public Space Space => fort; 

        public override bool Can() => fort != null && base.Can() && fort.damaged == true && 
            (fort.Flag != Player.Opponent.Faction || fort.adjacentSpaces.Where(space => space.control == Player.Faction).Any(space => space is Market || space is NavalSpace));

        public void SetSpace(Space space) => this.fort = space is Fort ? (Fort)space : null;
        public override bool Eligible(Space space) => space is Fort fort && fort.damaged; 

        protected override Task Do()
        {
            Commands.Push(new RemoveDamageMarkerCommand(fort));
            Commands.Push(new FlagSpaceCommand(fort, Player.Faction));

            return Task.CompletedTask;
        }

        public override void Setup(Player player)
        {
            Name = "Repair Fort";
        }
    }
}