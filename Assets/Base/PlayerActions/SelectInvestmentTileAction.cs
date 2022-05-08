using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Sirenix.OdinInspector; 

public class SelectInvestmentTileAction : PlayerAction, IAdjustActionPoints
{
    public InvestmentTile investmentTile;
    UnityAction callback;
    SelectionController selectionController;

    public ActionPoints actionPoints => investmentTile.actionPoints;

    Player IAdjustActionPoints.player => player;

    protected override void Do(UnityAction callback)
    {
        this.callback = callback;
        selectionController = FindObjectOfType<SelectionController>();
        player = Player.players[GetComponent<ActionRound>().actingFaction];

        selectionController.Summon(this, Phase.currentPhase.GetComponent<PeaceTurn>().investmentTiles.Where(kvp => kvp.Value == Game.Faction.Neutral).ToList(), 2);
        selectionController.SetTitle(actionText);
    }

    [Button]
    protected override void Finish(List<object> returns)
    {
        InvestmentTile tile = returns.First() as InvestmentTile; 
        Phase.currentPhase.GetComponent<PeaceTurn>().investmentTiles[tile] = player.faction;
        // TODO Get the Selection from the Selection Controller (eventually)

        player.actionPoints.AddRange(tile.actionPoints);

        callback.Invoke();
    }
}

public interface IAdjustActionPoints
{
    public ActionPoints actionPoints { get; }
    public Player player { get; }
}
