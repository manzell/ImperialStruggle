using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace ImperialStruggle
{
    public class BuildFortAction : PlayerAction, _PurchaseAction, TargetSpaceAction<Fort>
    {
        IEnumerable<Fort> eligibleForts;
        public ActionPoint ActionCost { get; private set; } 

        public Fort Space {get; private set;}
        public void SetSpace(Fort space) => Space = space;

        public BuildFortAction()
        {
            Name = "Build Fort";
            ActionRound.ActionRoundStartEvent += phase => eligibleForts = GetEligibleForts(phase.player.Faction);
        }

        public override bool Eligible(Space space) => space is Fort;

        public override bool Can(Player player)
        {
            if (Space == null) return false; 
            //Debug.Log($"BuildFortAction {fort?.Name} Base {base.Can()} Eligible {eligibleForts.Contains(fort)}");
            return base.Can(player) && eligibleForts.Contains(Space);
        }

        protected override Task Do(IAction context)
        {
            ActionCost = Space.flagCost.GetAPCost(Player, Space); 
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