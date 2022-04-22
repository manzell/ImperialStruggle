using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ActionRound))]
public class SelectInvestmentTilePhase : MonoBehaviour
{
    public UnityAction callback;

    public void Do(Phase phase, UnityAction callback)
    {
        Game.Faction faction = GetComponent<ActionRound>().actingFaction;
        this.callback = callback;
        Debug.Log($"{faction} to selects an Investment Tile");
    }

    public void Select(InvestmentTile tile)
    {
        Game.Faction faction = GetComponent<ActionRound>().actingFaction;
        Debug.Log($"{faction} selects the {tile} Investment Tile");

        //Phase.currentPhase.gameActions.Add(new SelectInvestmentTile(faction, tile));
    }
}
