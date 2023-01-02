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

        public ActionPoint ActionCost => new ActionPoint(market.Flag == Player.Opponent.Faction ? ActionPoint.ActionTier.Major : ActionPoint.ActionTier.Minor,
            ActionPoint.ActionType.Finance, market.GetFlagCost(Player) + (!market.Protected && market.Flag == Player.Opponent.Faction ? 1 : 0));

        public void SetSpace(Space space) => this.market = space is Market ? (Market)space : null;

        public override void Setup(Player player)
        {Name = "Shift Market";
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
                    (neighbor is Market targetMarket && neighbor.control == Player.Faction && !Isolated(targetMarket, Player)));
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

        public bool Isolated(Market market, Player player)
        {
            int counter = 99;
            bool retVal = true; 
            Space currentSpace = market;
            HashSet<Space> spacesToCheck = new();
            HashSet<Space> checkedSpaces = new();

            while (currentSpace != null && counter > 0)
            {
                counter--;
                if ((currentSpace is Territory || currentSpace is Fort || currentSpace is NavalSpace) && currentSpace.control == player.Faction)
                {
                    //Debug.Log($"{currentSpace.Name} is Eligible connection [{counter}]"); 
                    retVal = false;
                    break; 
                }
                else
                {
                    checkedSpaces.Add(currentSpace);

                    //Debug.Log($"{currentSpace.Name} is not Controlled Territory/Fort/NavalSpace. Adding " +
                    //    $"{string.Join(", ", spacesToAdd.Select(space => space.Name))} [{counter}]");

                    spacesToCheck.UnionWith(currentSpace.adjacentSpaces.Where(space => !checkedSpaces.Contains(space)
                        && space.Flag == market.Flag && !space.conflictMarker).Except(spacesToCheck));
                    currentSpace = spacesToCheck.FirstOrDefault();
                    spacesToCheck.Remove(currentSpace);
                }
            }

            Debug.Log($"Checking {market.Name} for Isolated Status: {retVal}");
            return retVal; 
        }

    }
}