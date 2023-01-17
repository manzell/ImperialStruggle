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
        [SerializeField] HashSet<PoliticalData> IrelandSpaces, ScotlandSpaces;


        Dictionary<Space, FlagCostCalculation> previousCalculations = new();

        public override void Reveal(Player player)
        {
            foreach (PoliticalSpace space in IrelandSpaces.Union(ScotlandSpaces).Select(data => Game.SpaceLookup[data]))
            {
                previousCalculations.Add(space, space.flagCost); 
                space.flagCost = new IrelandScotlandSwiftFlagCost(previousCalculations[space], this);
                space.updateSpaceEvent?.Invoke();
            }
            foreach(PoliticalSpace space in Game.Spaces.Where(space => space.map == Europe))
            {
                previousCalculations.Add(space, space.flagCost);
                space.flagCost = new EuropeDeflagSwiftFlagCost(previousCalculations[space], IrelandSpaces.Select(data => Game.SpaceLookup[data] as PoliticalSpace)); 
                space.updateSpaceEvent?.Invoke(); 
            }
        }

        protected override void Retire(Player player)
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
            IAction context; 

            public IrelandScotlandSwiftFlagCost(FlagCostCalculation calculation, IAction context)
            {
                this.context = context; 
                previousCalculation = calculation;
            }

            public override ActionPoint GetAPCost(Player player, FlaggableSpace space)
            {
                if (player.Faction == Game.Britain)
                    return new(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Diplomacy,
                        Mathf.Max(1, previousCalculation.GetAPCost(player, space).Value(context as _PurchaseAction) - 1)); 
                else
                    return previousCalculation.GetAPCost(player, space);
            }
        }

        public class EuropeDeflagSwiftFlagCost: FlagCostCalculation
        {
            FlagCostCalculation previousCalculation;
            HashSet<PoliticalSpace> irelandSpaces;
            bool canFlagWithMinorAP => irelandSpaces.Any(space => space.Control == Game.Britain);

            public EuropeDeflagSwiftFlagCost(FlagCostCalculation calculation, IEnumerable<PoliticalSpace> irelandSpaces)
            {
                previousCalculation = calculation;
                this.irelandSpaces = new(irelandSpaces);
            }

            public override ActionPoint GetAPCost(Player player, FlaggableSpace space)
            {
                if (canFlagWithMinorAP)
                    return new(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Diplomacy,
                        space.ConflictMarkers.Count > 0 ? 1 : (space.Data as PoliticalData).FlagCost);
                else
                    return previousCalculation.GetAPCost(player, space);
            }
        }
    }
}