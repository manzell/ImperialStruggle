using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class ShiftMarketAction : PlayerAction, RegionalPurchase, TargetSpaceAction
    {
        Market space;
        public Space Space => space;
        FlaggableSpace RegionalPurchase.Space => space;

        IEnumerable<Market> eligibleSpaces;

        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionType.Finance,
            space.Flag == Player.Opponent.Faction ? ActionPoint.ActionTier.Major : ActionPoint.ActionTier.Minor, 
            space.FlagCost + (!space.Protected && space.Flag == Player.Opponent.Faction ? 1 : 0));

        public void SetSpace(Space space) => this.space = space is Market ? (Market)space : null;

        public override void Setup()
        {
            eligibleSpaces = Game.Spaces.OfType<Market>().Where(market => market.adjacentSpaces.Any(adjacent => adjacent.control == Player.Faction)); 
        }

        public override bool Can() => base.Can() && space != null && eligibleSpaces.Contains(space) && 
            space.adjacentSpaces.Any(space =>
                ((space is Territory || space is Fort || space is NavalSpace) && space.Flag == Player.Faction) ||
                (space is Market targetMarket && space.control == Player.Faction && !targetMarket.isolated));

        protected override Task Do()
        {
            if(space.Flag == null)
                Commands.Push(new FlagSpaceCommand(space, Player.Faction)); 
            if(space.Flag == Player.Opponent.Faction)
                Commands.Push(new UnflagCommand(space)); 

            return Task.CompletedTask;
        }
    }
}