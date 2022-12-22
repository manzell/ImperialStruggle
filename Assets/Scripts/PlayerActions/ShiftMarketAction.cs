using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine; 

namespace ImperialStruggle
{
    public class ShiftMarketAction : PlayerAction, RegionalPurchase, TargetSpaceAction
    {
        Market market;
        public Space Space => market;
        FlaggableSpace RegionalPurchase.Space => market;

        IEnumerable<Market> eligibleMarkets;

        public ActionPoint ActionCost => new ActionPoint(market.Flag == Player.Opponent.Faction ? ActionPoint.ActionTier.Major : ActionPoint.ActionTier.Minor,
            ActionPoint.ActionType.Finance, market.GetFlagCost(Player) + (!market.Protected && market.Flag == Player.Opponent.Faction ? 1 : 0));

        public void SetSpace(Space space) => this.market = space is Market ? (Market)space : null;

        public override void Setup(Player player)
        {
            Name = "Shift Market"; 
            base.Setup(player);
            ActionRound.ActionRoundStartEvent += phase => eligibleMarkets = GetEligibleSpaces(phase.player.Faction);
        }

        public override bool Eligible(Space space) => space is Market; 
        public override bool Can()
        {
            if (market == null) return false;

            return base.Can() && market != null && eligibleMarkets.Contains(market) && market.adjacentSpaces.Any(space =>
                ((space is Territory || space is Fort || space is NavalSpace) && space.Flag == Player.Faction) ||
                (space is Market targetMarket && space.control == Player.Faction && !targetMarket.Isolated(Player)));
        }

        protected override Task Do()
        {
            if(market.Flag == null)
                Commands.Push(new FlagSpaceCommand(market, Player.Faction)); 
            if(market.Flag == Player.Opponent.Faction)
                Commands.Push(new UnflagCommand(market)); 

            return Task.CompletedTask;
        }

        IEnumerable<Market> GetEligibleSpaces(Faction faction) => Game.Spaces.OfType<Market>().Where(market => market.adjacentSpaces.Any(adjacent => adjacent.control == faction));
    }
}