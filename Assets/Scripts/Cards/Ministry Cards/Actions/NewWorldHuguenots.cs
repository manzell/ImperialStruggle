using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class NewWorldHuguenots : MinisterAction
    {
        [SerializeField] List<Map> maps;
        List<Territory> selectedTerritories = new();

        protected override async Task Do()
        {
            HashSet<Territory> territories = new(maps.SelectMany(map => map.spaces.OfType<Territory>()
                .Where(territory => territory.Flag == Game.France && !selectedTerritories.Contains(territory))));

            await new Selection<Territory>(Player, territories, PlaceMarker).Completion;
        }

        void PlaceMarker(Selection<Territory> SelectedTerritories)
        {
            Territory territory = SelectedTerritories.FirstOrDefault();

            if (territory != null)
            {
                selectedTerritories.Add(territory);

                territory.Actions.Add(new FlipHuguenotsAction(territory)); 

                Commands.Push(new AdjustCPCostCommand(territory, 1)); 
            }
        }
    }

    public class FlipHuguenotsAction : PlayerAction
    {
        Territory territory;

        public FlipHuguenotsAction(Territory territory)
        {
            this.territory = territory;
            Name = "Flip New World Huguenots Token"; 
        }

        protected override Task Do()
        {
            territory.Actions.Remove(this);

            Game.Spaces.Where(space => space.map == territory.map && space.data.Region == territory.data.Region).OfType<Market>()
                .ForEach(market => market.flagCost = new HuguenotsMarketFlagCost(market.flagCost)); 

            return Task.CompletedTask; 
        }
    }

    public class HuguenotsMarketFlagCost : FlagCostCalculation
    {
        FlagCostCalculation baseCalculation; 

        public HuguenotsMarketFlagCost(FlagCostCalculation baseCalculation)
        {
            this.baseCalculation = baseCalculation;
        }

        public override ActionPoint GetAPCost(Player player, FlaggableSpace space)
        {
            if(player.Faction == Game.France)
                return new ActionPoint(space.Flag == player.Opponent.Faction ? ActionPoint.ActionTier.Major : ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Finance,
                    space.ConflictMarker ? 1 : (space.Data as MarketData).FlagCost - 1);
            else
                return baseCalculation.GetAPCost(player, space);
        }
    }
}
