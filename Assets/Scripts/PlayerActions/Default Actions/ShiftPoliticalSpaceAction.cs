using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public class ShiftPoliticalSpaceAction : PlayerAction, RegionalPurchase<PoliticalSpace>
    {
        public PoliticalSpace Space { get; private set; }        
        public void SetSpace(PoliticalSpace space) => Space = space;
        public ActionPoint ActionCost => Space.flagCost.GetAPCost(Player, Space); 

        public override bool Eligible(Space space) => space is PoliticalSpace;

        protected override Task Do(IAction context)
        {
            if (Space.Flag == null)
                Commands.Push(new FlagSpaceCommand(Space, Player.Faction));
            if (Space.Flag == Player.Opponent.Faction)
                Commands.Push(new UnflagCommand(Space));

            return Task.CompletedTask;
        }

        public ShiftPoliticalSpaceAction()
        {
            Name = Space.Flag == Game.Neutral ? "Flag" : "Unflag";
        }
    }
}
