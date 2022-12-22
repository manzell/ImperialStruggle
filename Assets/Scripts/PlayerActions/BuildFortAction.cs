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
        Fort fort;
        IEnumerable<Fort> eligibleForts;
        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Military, fort.GetFlagCost(Player));

        public Space Space => fort;
        public void SetSpace(Space space) => this.fort = space is Fort ? (Fort)space : null;

        public override bool Can()
        {
            if (fort == null) return false; 
            //Debug.Log($"BuildFortAction {fort?.Name} Base {base.Can()} Eligible {eligibleForts.Contains(fort)}");
            return fort != null && base.Can() && eligibleForts.Contains(fort);
        }

        public override void Setup(Player player)
        {
            Name = "Build Fort"; 
            base.Setup(player);
            ActionRound.ActionRoundStartEvent += phase => eligibleForts = GetEligibleForts(player.Faction);
        }

        public override bool Eligible(Space space) => space is Fort; 

        protected override Task Do()
        {
            Commands.Push(new FlagSpaceCommand(fort, Player.Faction));
            return Task.CompletedTask; 
        }

        IEnumerable<Fort> GetEligibleForts(Faction faction)
        {
            return Game.Spaces.Where(space => space.adjacentSpaces.Any(s => 
                (s is Market || s is NavalSpace || s is Territory) && s.control == faction)).OfType<Fort>();
        }
    }
}