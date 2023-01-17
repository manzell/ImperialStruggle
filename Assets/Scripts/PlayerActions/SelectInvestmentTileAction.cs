using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class SelectInvestmentTileAction : PlayerAction
    {
        [SerializeField] Calculation<IEnumerable<InvestmentTile>> tiles; 

        protected async override Task Do(IAction context)
        {
            if (Phase.CurrentPhase is ActionRound actionRound)
            {
                Selection<InvestmentTile> selection = new(actionRound.player, tiles.Calculate(context));
                selection.SetTitle("Select Investment Tile");

                await selection.Completion;
            }
        }
    }

    public class SelectInvestmentTileResponse : SelectionReceiver<ISelectable> 
    {
        public override async void OnSelect(Selection<ISelectable> selection)
        {
            if (Phase.CurrentPhase is ActionRound actionRound)
            {
                InvestmentTile tile = selection.First() as InvestmentTile;
                Debug.Log($"{tile.Name} Selected by {selection.player.Faction}");

                Commands.Append(new SelectInvestmentTileCommand(actionRound.player, tile));

                foreach (PlayerAction action in tile.actions)
                {
                    if (action.Can(selection.player))
                        await action.Execute();
                }
            }
        }
    }

    public class AvailableInvestmentTilesCalc : Calculation<IEnumerable<ISelectable>>
    {
        protected override IEnumerable<ISelectable> Calc(IAction context)
        {             
            PeaceTurn peaceTurn = Phase.CurrentPhase.GetComponentInParent<PeaceTurn>();
            return peaceTurn.investmentTiles.Where(kvp => kvp.Value == Game.Neutral).Select(kvp => kvp.Key);
        }
    }
}