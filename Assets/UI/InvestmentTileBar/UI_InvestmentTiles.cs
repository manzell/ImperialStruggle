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
    }

    void SortTiles()
    {

    }

    void RemoveTiles()
    {
        foreach(Transform child in transform)
            Destroy(child.gameObject);
    }

    void AddTiles(List<InvestmentTile> tiles)
    {
        tiles = tiles.OrderBy(tile => tile).ToList();

        foreach(InvestmentTile tile in tiles)
        {
            GameObject prefab = Instantiate(investmentTilePrefab, target.transform);
            prefab.GetComponent<UI_InvestmentTile>().SetTile(tile);
        }
    }
}
