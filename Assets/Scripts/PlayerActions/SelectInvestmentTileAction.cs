using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class SelectInvestmentTileAction : PlayerAction
    {
        protected async override Task Do()
        {
            if (Phase.CurrentPhase is ActionRound actionRound)
            {
                PeaceTurn peaceTurn = actionRound.GetComponentInParent<PeaceTurn>();
                IEnumerable<InvestmentTile> tiles = peaceTurn.investmentTiles.Where(kvp => kvp.Value == Game.Neutral).Select(kvp => kvp.Key);

                Selection<InvestmentTile> selection = new(actionRound.player, tiles, Finish);
                selection.SetTitle("Select Investment Tile");

                await selection.Completion;
            }
        }

        async void Finish(Selection<InvestmentTile> tiles)
        {
            if(Phase.CurrentPhase is ActionRound actionRound)
            {
                InvestmentTile tile = tiles.First(); 
                Debug.Log($"{tile.Name} Selected by {Player.Faction}"); 

                Commands.Push(new SelectInvestmentTileCommand(actionRound.player, tile));

                foreach(PlayerAction action in tile.actions)
                {
                    action.Setup(Player); 

                    if(action.Can())
                        await action.Execute();
                }
            }
        }
    }
}