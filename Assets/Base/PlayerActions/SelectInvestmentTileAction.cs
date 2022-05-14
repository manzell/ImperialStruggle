using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Sirenix.OdinInspector; 

public class SelectInvestmentTileAction : PlayerAction, IAdjustAP
{
    public InvestmentTile investmentTile;
    SelectionController.Selection<InvestmentTile> selection;

    public ActionPoints actionPoints => investmentTile.actionPoints;

    Player IAdjustAP.player => player;

    protected override void Do(UnityAction callback)
    {
        player = Player.players[GetComponent<ActionRound>().actingFaction];
        Dictionary<InvestmentTile, Game.Faction> investmentTiles = Phase.currentPhase.GetComponentInParent<PeaceTurn>().investmentTiles; 

        selection = FindObjectOfType<SelectionController>().Select(investmentTiles.Where(kvp => kvp.Value == Game.Faction.Neutral).Select(kvp => kvp.Key).ToList(), 1);
        selection.SetTitle($"Select Investment Tile for {player.faction}");
        selection.callback = returns => InvestmentTileActions(returns, callback);
    }

    [Button]
    void InvestmentTileActions(List<InvestmentTile> returns, UnityAction callback)
    {
        if(returns.Count == 1)
        {
            investmentTile = returns.First();

            foreach (PlayerAction action in investmentTile.GetComponents<PlayerAction>())
                action.player = player;

            Phase.RunActionSequence(investmentTile.GetComponents<PlayerAction>().ToList<BaseAction>(), () => callback.Invoke());

            base.Do(() => { });
        }
        else
        {
            // Send a warning that 1 Investment Tile is required
        }
    }
}
