using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI; 

public class UI_InvestmentTiles : MonoBehaviour
{
    [SerializeField] GameObject investmentTilePrefab;
    [SerializeField] GameObject target; 

    private void Awake()
    {
        DealInvestmentTileCommand.dealInvestmentTileEvent.AddListener(AddTile);
        Phase.phaseEndEvent.AddListener(ClearInvestmentTiles); 
    }

    void AddTile(InvestmentTile tile)
    {
        GameObject prefab = Instantiate(investmentTilePrefab, target.transform);
        prefab.GetComponent<UI_InvestmentTile>().SetTile(tile);
    }

    void ClearInvestmentTiles(Phase phase)
    {
        if(phase.GetComponent<PeaceTurn>())
        {
            foreach (Transform child in target.transform)
                Destroy(child.gameObject); 
        }
    }
}