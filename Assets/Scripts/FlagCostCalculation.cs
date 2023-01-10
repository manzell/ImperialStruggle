using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public abstract class FlagCostCalculation
    {
        public abstract ActionPoint GetAPCost(Player player, FlaggableSpace space); 
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
            Market market = space as Market; 

            return new(space.Flag == player.Opponent.Faction ? ActionPoint.ActionTier.Major : ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Military,
                (market.Protected && space.Flag == player.Opponent.Faction ? 1 : 0) + (space.ConflictMarkers.Count > 0 || market.Isolated(player) ? 1 : (space.Data as PoliticalData).FlagCost));
        }
    }
}
