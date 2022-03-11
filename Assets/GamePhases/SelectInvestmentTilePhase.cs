using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ActionRound))]
public class SelectInvestmentTilePhase : MonoBehaviour, IPhaseAction
{
    public static UnityEvent<Game.Faction, InvestmentTile> selectInvestmentTileEvent = new UnityEvent<Game.Faction, InvestmentTile>();
    public UnityAction callback;

    public void Do(Phase phase, UnityAction callback) =>
        this.callback = callback;

    [Sirenix.OdinInspector.Button(Name ="Select Investment Tile")]
    public void Select(InvestmentTile tile)
    {
        Game.Faction faction = GetComponent<ActionRound>().actingFaction;
        Debug.Log($"{faction} selects the {tile} Investment Tile");

        Phase.currentPhase.gameActions.Add(new SelectInvestmentTile(faction, tile));
        selectInvestmentTileEvent.Invoke(faction, tile);

        callback.Invoke();
    }
}
