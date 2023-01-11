using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class AdjustFlagCostAdvantageAction : PlayerAction
    {
        [SerializeField] Calculation<HashSet<Space>> eligibleSpaces; 
        [SerializeField] int flagCostAdjustment; 

        Dictionary<FlaggableSpace, FlagCostCalculation> previousFlagCosts = new(); 

        protected override Task Do()
        {
            foreach(FlaggableSpace space in eligibleSpaces.Calculate(Player))
            {
                previousFlagCosts.Add(space, space.flagCost);

                ActionPoint newFlagCost = new(space.flagCost.GetAPCost(Player, space));
                newFlagCost.AdjustBaseValue(flagCostAdjustment);

                space.flagCost = new ReducedFlagCostCalculation(Player, newFlagCost, space.flagCost);

                // TODO - Ensure this gets called AFTER the player is charged (which happens via Execute
                space.ShiftSpaceEvent += playerAction => space.flagCost = previousFlagCosts[space]; 
            }

            return Task.CompletedTask; 
        }
    }

    public class ReducedFlagCostCalculation : FlagCostCalculation
    {
        public Player player;
        FlagCostCalculation previous; 

        public ReducedFlagCostCalculation(Player player, ActionPoint AP, FlagCostCalculation previous)
        {
            this.AP = AP;
            this.player = player;
            this.previous = previous;
        }

        public override ActionPoint GetAPCost(Player player, FlaggableSpace space) => player == this.player ? AP : previous.GetAPCost(player, space); 
    }
}
