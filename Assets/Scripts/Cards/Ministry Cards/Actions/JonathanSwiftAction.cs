using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class JonathanSwiftAction : MinisterAction
    {
        [SerializeField] Map Europe; 
        [SerializeField] HashSet<PoliticalSpace> IrelandSpaces, ScotlandSpaces;

        bool canFlagWithMinorAP => IrelandSpaces.Any(space => space.control == Game.France);
        Dictionary<Space, FlagCostCalculation> previousCalculations = new();

        public override void Reveal()
        {
            foreach (PoliticalSpace space in IrelandSpaces.Union(ScotlandSpaces))
            {
                previousCalculations.Add(space, space.flagCost); 
                space.flagCost = new IrelandScotlandSwiftFlagCost(previousCalculations[space]);
                space.updateSpaceEvent?.Invoke();
            }
            foreach(PoliticalSpace space in Europe.spaces.Except(IrelandSpaces).Except(ScotlandSpaces))
            {
                previousCalculations.Add(space, space.flagCost);
                space.flagCost = new EuropeDeflagSwiftFlagCost(previousCalculations[space], IrelandSpaces);
                space.updateSpaceEvent?.Invoke(); 
            }
        }

        protected override void Retire()
        {
            foreach (PoliticalSpace space in previousCalculations.Keys)
            {
                space.flagCost = previousCalculations[space];
                space.updateSpaceEvent?.Invoke(); 
            }
        }

        public class IrelandScotlandSwiftFlagCost : FlagCostCalculation
        {
            FlagCostCalculation previousCalculation;

            public IrelandScotlandSwiftFlagCost(FlagCostCalculation calculation) => previousCalculation = calculation; 

            public override ActionPoint GetAPCost(Player player, FlaggableSpace space)
            {
                if (player.Faction == Game.Britain)
                    return new(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Diplomacy,
                        space.ConflictMarker ? 1 : (space.Data as PoliticalData).FlagCost - 1);
                else
                    return previousCalculation.GetAPCost(player, space); 
            }
        }

        public class EuropeDeflagSwiftFlagCost: FlagCostCalculation
        {
            FlagCostCalculation previousCalculation;
            HashSet<PoliticalSpace> irelandSpaces;

            public EuropeDeflagSwiftFlagCost(FlagCostCalculation calculation, HashSet<PoliticalSpace> irelandSpaces)
            {
                previousCalculation = calculation;
                this.irelandSpaces = irelandSpaces; 
            }

            public override ActionPoint GetAPCost(Player player, FlaggableSpace space)
            {
                if (player.Faction == Game.Britain && space.Flag == Game.France && irelandSpaces.Any(space => space.control == Game.Britain))
                    return new(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Diplomacy,
                        space.ConflictMarker ? 1 : (space.Data as PoliticalData).FlagCost);
                else
                    return previousCalculation.GetAPCost(player, space);
            }
        }
    }
}
