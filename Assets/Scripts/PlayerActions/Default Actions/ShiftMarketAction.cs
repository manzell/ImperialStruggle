using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class ShiftMarketAction : PlayerAction, RegionalPurchase<Market>
    {
        public ShiftType shiftType { get; private set; }
        public Market Space { get; private set; }
        public ActionPoint ActionCost => Space.flagCost.GetAPCost(Player, Space);
        HashSet<Market> eligibleMarkets;

        public ShiftMarketAction()
        {
            if (Space.Flag == Game.Neutral)
            {
                shiftType = ShiftType.Flag; 
                Name = "Flag Market";
            }
            else if(Space.Flag == Player.Opponent.Faction)
            {
                shiftType = ShiftType.Unflag;
                Name = "Unflag Market"; 
            }
            ActionRound.ActionRoundStartEvent += SetEligibleSpaces;                
        }

        public void SetSpace(Market space) => Space = space;
        public override bool Eligible(Space space) => space is Market; 
        public override bool Can(Player player)
        {
            if (Space == null) return false;

            return base.Can(player) && eligibleMarkets.Contains(Space) && Space.adjacentSpaces.Any(neighbor =>
                    ((neighbor is Territory || neighbor is Fort || neighbor is NavalSpace) && neighbor.Flag == Player.Faction) ||
                    (neighbor is Market targetMarket && neighbor.Control == Player.Faction && !targetMarket.Isolated()));
        }

        protected override Task Do(IAction context)
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