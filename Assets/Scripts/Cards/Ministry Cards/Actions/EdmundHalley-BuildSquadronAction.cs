using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

namespace ImperialStruggle
{
    public class EdmundHalleyBuildSquadronAction : MinisterAction, PurchaseAction
    {
        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Military, 2);

        public override Task Do(Player player)
        {
            Exhausted = true;
            Commands.Push(new BuildFleetCommand(player)); 
            return Task.CompletedTask;
        }
    }

    public class EdmundHalleyTradeEventCard : MinisterAction
    {
        [SerializeField] Map europe; 

        protected override bool Can(Player player) => player.Squadrons.Any(squadron => squadron.space?.map == europe) && player.Cards.Count() > 0
            && base.Can(player);

        public async override Task Do(Player player)
        {
            await new Selection<EventCard>(player, player.Cards, Finish).Completion; 
        }

        void Finish(Selection<EventCard> selection)
        {
            if(selection.Count() > 0)
            {
                Commands.Push(new DiscardCardCommand(selection.player, selection.First())); 
                Commands.Push(new AdjustTreatyPointsCommand(selection.player.Faction, 1));
                Exhausted = true; 
            }
        }
    }
}
