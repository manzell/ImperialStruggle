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

    ActionPoints actionPoints = new ActionPoints();

    private void Awake()
    {
        AdjustAPCommand.adjustAPEvent.AddListener(UpdateTiles);
        Game.setActivePlayerEvent.AddListener(player => actionPoints = player.actionPoints); 
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

    public void UpdateTiles()
    {
        Debug.Log("Update Tiles!"); 
        // Sets our current Display to match the given set of Action Points

        /* First let's get a list of each KEY in our suitcase of action points,
         * then sort them how we like */

        List<ActionPoint.ActionPointKey> ourKeys = actionPoints.suitcase.Keys.ToList();

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