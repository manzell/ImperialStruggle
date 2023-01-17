using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

namespace ImperialStruggle
{
    public class EdmundHalleyBuildSquadronAction : MinisterAction, _PurchaseAction
    {
        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Military, 2);

        protected override Task Do(IAction context)
        {
            Exhausted = true;
            Commands.Push(new BuildFleetCommand(Player)); 
            return Task.CompletedTask;
        }
    }

    public class EdmundHalleyTradeEventCard : MinisterAction
    {
        [SerializeField] Map europe; 

        public override bool Can(Player player) => Player.Squadrons.Any(squadron => squadron.space?.map == europe) && Player.Cards.Count() > 0
            && base.Can(player);

        protected async override Task Do(IAction context)
        {
            await new Selection<EventCard>(Player, Player.Cards, Finish).Completion; 
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
