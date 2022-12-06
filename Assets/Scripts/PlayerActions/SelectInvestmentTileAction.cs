using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Sirenix.OdinInspector; 

public class SelectInvestmentTileAction : PlayerAction
{
    SelectionController.Selection selection;

    protected override void Do()
    {
        if(Phase.CurrentPhase is ActionRound actionRound)
        {
            IEnumerable<InvestmentTile> tiles = actionRound.GetComponentInParent<PeaceTurn>().investmentTiles.Where(kvp => kvp.Value == null).Select(kvp => kvp.Key);
            selection = FindObjectOfType<SelectionController>().Select(tiles.ToList<ISelectable>(), 1);
            selection.SetTitle($"Select Investment Tile for {actingPlayer.faction}");
            selection.callback = InvestmentTileActions;
        }
    }

    [Button]
    void InvestmentTileActions(List<ISelectable> returns)
    {
        commands.Push(new SelectInvestmentTileCommand((InvestmentTile)returns.First(), actingPlayer.faction));
    }
}
