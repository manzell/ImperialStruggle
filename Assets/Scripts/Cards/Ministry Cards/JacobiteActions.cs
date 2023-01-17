using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class JacobiteShiftAction : MinisterAction, _PurchaseAction
    {
        [SerializeField] HashSet<PoliticalData> jacobiteSpaces;
        public ActionPoint ActionCost { get; set; }

        protected override async Task Do(IAction context)
        {
            Selection<PoliticalSpace> selection = new(Player, jacobiteSpaces.Select(data => Game.SpaceLookup[data] as PoliticalSpace).Where(space => space.Flag != Player.Faction));

            await selection.Completion; 

            if(selection.Count() > 0)
            {
                int flagCost = selection.First().flagCost.GetAPCost(Player, selection.First()).Value(this);

                ActionCost = new(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Military, flagCost);

                Commands.Push(new ShiftSpaceCommand(selection.First(), Player.Faction)); 
            }
        }
    }

    public class ScoreJacobitesAction : MinisterAction, _PurchaseAction
    {
        [SerializeField] HashSet<PoliticalData> scoringSpaces; 
        public ActionPoint ActionCost => new(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Military, 3);

        protected override Task Do(IAction context)
        {
            int VP = Mathf.Min(4, scoringSpaces.Count(data => Game.SpaceLookup[data].Flag == Game.France) + Jacobites.VictoryMarkers);

            Commands.Push(new AdjustVPCommand(Game.France, VP));
            
            return Task.CompletedTask; 
        }
    }
}
