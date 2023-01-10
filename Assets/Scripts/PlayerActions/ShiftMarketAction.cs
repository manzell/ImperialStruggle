using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine; 

namespace ImperialStruggle
{
    public class ShiftMarketAction : PlayerAction, RegionalPurchase<Market>
    {
        public Market Space { get; private set; }
        public ActionPoint ActionCost => Space.flagCost.GetAPCost(Player, Space);
        HashSet<Market> eligibleMarkets;

        public override void Setup(Player player)
        {
            Name = "Shift Market";
            base.Setup(player);
            ActionRound.ActionRoundStartEvent += SetEligibleSpaces;                
        }

        public void SetSpace(Market space) => Space = space;
        public override bool Eligible(Space space) => space is Market; 
        public override bool Can()
        {
            if (Space == null) return false;

            return base.Can() && eligibleMarkets.Contains(Space) && Space.adjacentSpaces.Any(neighbor =>
                    ((neighbor is Territory || neighbor is Fort || neighbor is NavalSpace) && neighbor.Flag == Player.Faction) ||
                    (neighbor is Market targetMarket && neighbor.Control == Player.Faction && !targetMarket.Isolated(Player)));
        }

        protected override Task Do()
        {
            if(Space.Flag == Game.Neutral)
                Commands.Push(new FlagSpaceCommand(Space, Player.Faction)); 
            if(Space.Flag == Player.Opponent.Faction)
                Commands.Push(new UnflagCommand(Space)); 

            return Task.CompletedTask;
        }

        void SetEligibleSpaces(ActionRound ar)
        {
            eligibleMarkets = new(); 

            if (ar.player == Player)
                foreach (Market market in Game.Spaces.OfType<Market>().Where(m => m.adjacentSpaces.Any(space => space.Control == Player.Faction)))
                    eligibleMarkets.Add(market);
        }
    }
}