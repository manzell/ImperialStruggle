using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector; 

public class UI_ActionPoints : SerializedMonoBehaviour
{
    [SerializeField] GameObject actionPointPrefab;
    [SerializeField] Dictionary<ActionPoint.ActionTier, GameObject> actionTiers = new Dictionary<ActionPoint.ActionTier, GameObject>(); 
    
    Dictionary<ActionPoint.ActionPointKey, UI_ActionPoint> APtiles = new Dictionary<ActionPoint.ActionPointKey, UI_ActionPoint>();

    ActionPoints actionPoints; 

    void AddTile(ActionPoint actionPoint)
    {
        GameObject tile = Instantiate(actionPointPrefab, actionTiers[actionPoint.actionTier].transform);
        APtiles.Add(actionPoint.apKey, tile.GetComponent<UI_ActionPoint>());


        APtiles[actionPoint.apKey].SetDisplay(actionPoint.apKey, actionPoints.Values[actionPoint.apKey]); 
    }

    void AddTile(ActionPoint.ActionPointKey key)
    {
        //ActionPoint actionPoint = new ActionPoint(key.actionType, key.actionTier);
        //AddTile(actionPoint);


        GameObject tile = Instantiate(actionPointPrefab, actionTiers[key.actionTier].transform);
        APtiles.Add(key, tile.GetComponent<UI_ActionPoint>());
        APtiles[key].SetDisplay(key, actionPoints.Values[key]);
    }

    void RemoveTile(ActionPoint.ActionPointKey key)
    {
        Destroy(APtiles[key].transform);
        APtiles.Remove(key); 
    }

    public void UpdateTiles(ActionPoints actionPoints)
    {
        // Sets our current Display to match the given set of Action Points

        /* First let's get a list of each KEY in our suitcase of action points,
         * then sort them how we like */

        List<ActionPoint.ActionPointKey> ourKeys = actionPoints.suitcase.Keys.ToList();
        this.actionPoints = actionPoints; 

        foreach(ActionPoint.ActionPointKey key in APtiles.Where(kvp => !ourKeys.Contains(kvp.Key)).Select(kvp => kvp.Key))
            RemoveTile(key); 

        /* now we loop through ourKeys, and if we have the key in APTiles, update it's value, otherwise we create it
         */
        foreach(ActionPoint.ActionPointKey key in ourKeys)
        {
            if (APtiles.ContainsKey(key))
                APtiles[key].setAPValue(key, actionPoints.Values[key]); 
            else
                AddTile(key); 
        }

    }

}