using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI; 

public class UI_InvestmentTiles : MonoBehaviour
{
    [SerializeField] GameObject investmentTilePrefab;
    [SerializeField] GameObject target; 

    List<InvestmentTile> investmentTiles = new List<InvestmentTile>();

    private void Awake()
    {
        SelectInvestmentTile.selectInvestmentTileEvent.AddListener(tile => SortTiles());
        Phase.phaseEndEvent.AddListener(phase => { if (phase is PeaceTurn) RemoveTiles(); });
        DealInvestmentTiles.dealInvestmentTilesEvent.AddListener(AddTiles); 
    }

    void SortTiles()
    {
        investmentTiles = investmentTiles.OrderBy(tile => {
            int actionRoundSelected = 0; 

            if(Phase.currentPhase is ActionRound)
                foreach(ActionRound ar in Phase.currentPhase.transform.parent.GetComponentsInChildren<ActionRound>())
                    if (ar.investmentTile == tile) actionRoundSelected = ar.transform.GetSiblingIndex(); 

            return (tile.available, actionRoundSelected); 
        }).ToList(); 

        for(int i = investmentTiles.Count - 1; i >= 0; i--)
            investmentTiles[i].gameObject.transform.SetSiblingIndex(i); 
    }

    void RemoveTiles()
    {
        foreach(Transform child in transform)
            Destroy(child.gameObject);
    }

    void AddTiles(List<InvestmentTile> tiles)
    {
        tiles = tiles.OrderBy(tile => (tile.majorAction.First().Key, tile.minorAction.First().Key, 5 - tile.majorAction.First().Value)).ToList();

        foreach(InvestmentTile tile in tiles)
        {
            GameObject prefab = Instantiate(investmentTilePrefab, target.transform);
            prefab.GetComponent<UI_InvestmentTile>().SetTile(tile);
        }
    }
}
