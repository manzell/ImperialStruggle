using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public abstract class FlagCostCalculation
    {
        [SerializeField] protected ActionPoint AP; 
        public abstract ActionPoint GetAPCost(Player player, FlaggableSpace space);
        public void SetAPCost(ActionPoint AP) => this.AP = AP;  
    }

    public class DefaultPoliticalFlagCost : FlagCostCalculation
    {
        public override ActionPoint GetAPCost(Player player, FlaggableSpace space)
        {
            return new(space.Flag == player.Opponent.Faction ? ActionPoint.ActionTier.Major : ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Diplomacy,
                space.ConflictMarkers.Count > 0 ? 1 : (space.Data as PoliticalData).FlagCost);
        }
    }

    public class DefaultMarketFlagCost : FlagCostCalculation
    {
        public override ActionPoint GetAPCost(Player player, FlaggableSpace space)
        {
            return new(space.Flag == player.Opponent.Faction ? ActionPoint.ActionTier.Major : ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Finance,
                space.ConflictMarkers.Count > 0 ? 1 : (space.Data as MarketData).FlagCost);
        }
    }

    public class DefaultFortFlagCost : FlagCostCalculation
    {
        public override ActionPoint GetAPCost(Player player, FlaggableSpace space)
        {
            Fort fort = space as Fort;

            return new(space.Flag == player.Opponent.Faction ? ActionPoint.ActionTier.Major : ActionPoint.ActionTier.Minor,
                ActionPoint.ActionType.Military,
                (fort.Data as FortData).FlagCost + (space.Flag == player.Opponent.Faction ? 1 : 0) - (space.Flag == player.Faction ? 1 : 0)); 
        }
    }
}
