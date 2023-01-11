using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace ImperialStruggle
{
    public class BuildFortAction : PlayerAction, PurchaseAction, TargetSpaceAction<Fort>
    {
        IEnumerable<Fort> eligibleForts;
        public ActionPoint ActionCost => Space.flagCost.GetAPCost(Player, Space); 

        public Fort Space {get; private set;}
        public void SetSpace(Fort space) => Space = space;

        public override bool Can()
        {
            if (Space == null) return false; 
            //Debug.Log($"BuildFortAction {fort?.Name} Base {base.Can()} Eligible {eligibleForts.Contains(fort)}");
            return base.Can() && eligibleForts.Contains(Space);
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
            Commands.Push(new FlagSpaceCommand(Space, Player.Faction));
            return Task.CompletedTask; 
        }

        IEnumerable<Fort> GetEligibleForts(Faction faction)
        {
            return Game.Spaces.Where(space => space.adjacentSpaces.Any(s => 
                (s is Market || s is NavalSpace || s is Territory) && s.Control == faction)).OfType<Fort>();
        }
    }
}