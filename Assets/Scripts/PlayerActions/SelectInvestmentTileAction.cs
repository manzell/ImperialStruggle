using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
    public class SelectInvestmentTileAction : PlayerAction
    {
        protected override void Do()
        {
            if (Phase.CurrentPhase is ActionRound actionRound)
            {
                IEnumerable<InvestmentTile> tiles = actionRound.GetComponentInParent<PeaceTurn>().investmentTiles.Where(kvp => kvp.Value == null).Select(kvp => kvp.Key);

                Selection<InvestmentTile> selection = new(actingPlayer, tiles, InvestmentTileActions);
                //selection.SetTitle($"Select Investment Tile for {actingPlayer.faction}");
            }
        }

        [Button]
        void InvestmentTileActions(Selection<InvestmentTile> selection)
        {
            commands.Add(new SelectInvestmentTileCommand(selection.First(), actingPlayer.faction));
        }
    }
}