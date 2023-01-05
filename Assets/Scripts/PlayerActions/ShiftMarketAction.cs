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

        HashSet<Market> eligibleMarkets;

        public ActionPoint ActionCost => market.flagCost.GetAPCost(Player, market);

        public void SetSpace(Space space) => this.market = space is Market ? (Market)space : null;

        public override void Setup(Player player)
        {
            Name = "Shift Market";
            base.Setup(player);
            ActionRound.ActionRoundStartEvent += SetEligibleSpaces;                
        }

        public override bool Eligible(Space space) => space is Market; 
        public override bool Can()
        {
            if (market == null) return false;

            return base.Can() && market != null && eligibleMarkets.Contains(market) && 
                market.adjacentSpaces.Any(neighbor =>
                    ((neighbor is Territory || neighbor is Fort || neighbor is NavalSpace) && neighbor.Flag == Player.Faction) ||
                    (neighbor is Market targetMarket && neighbor.control == Player.Faction && !targetMarket.Isolated(Player)));
        }

        protected override Task Do()
        {
            if(market.Flag == Game.Neutral)
                Commands.Push(new FlagSpaceCommand(market, Player.Faction)); 
            if(market.Flag == Player.Opponent.Faction)
                Commands.Push(new UnflagCommand(market)); 

            return Task.CompletedTask;
        }

        void SetEligibleSpaces(ActionRound ar)
        {
            eligibleMarkets = new(); 

            if (ar.player == Player)
                foreach (Market market in Game.Spaces.OfType<Market>().Where(m => m.adjacentSpaces.Any(space => space.control == Player.Faction)))
                    eligibleMarkets.Add(market);
        }
    }
}