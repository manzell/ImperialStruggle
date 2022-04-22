using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class DealInvestmentTiles : MonoBehaviour
{
    public int numInvestmentTiles = 9; 
    Dictionary<Map, AwardTile> prevInvestmentTiles = new Dictionary<Map, AwardTile>();
    public static UnityEvent<List<InvestmentTile>> dealInvestmentTilesEvent = new UnityEvent<List<InvestmentTile>>();

    public void Do(Phase phase, UnityAction callback)
    {
        List<InvestmentTile> investmentTiles = FindObjectsOfType<InvestmentTile>().ToList();
        PeaceTurn peaceTurn = phase.GetComponent<PeaceTurn>();

        if (investmentTiles.Where(tile => tile.exhausted == false).Count() < numInvestmentTiles)
        {
            investmentTiles.ForEach(tile => tile.exhausted = false);
            Debug.Log("Reshuffling Investment Tiles");
        }

        IEnumerable<InvestmentTile> availInvestmentTiles = investmentTiles.Where(tile => tile.exhausted == false).OrderBy(t => Random.value).Take(numInvestmentTiles);

        peaceTurn.investmentTiles.Clear();

        foreach(InvestmentTile tile in peaceTurn.investmentTiles.Keys)
        {
            tile.exhausted = false;
            tile.available = true;
            Debug.Log($"{tile} added to Investment Tile pool");
        }

        dealInvestmentTilesEvent.Invoke(peaceTurn.investmentTiles.Keys.ToList());
        callback.Invoke();
    }
}
