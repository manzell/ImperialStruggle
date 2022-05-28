using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector; 

public class UI_ActionPoints : SerializedMonoBehaviour
{
    [SerializeField] GameObject actionPointPrefab;
    [SerializeField] Dictionary<ActionPoint.ActionTier, GameObject> actionTiers = new Dictionary<ActionPoint.ActionTier, GameObject>(); 
    
    Dictionary<string, UI_ActionPoint> APtiles = new Dictionary<string, UI_ActionPoint>();

    ActionPoints actionPoints;
    Player activePlayer; 

    private void Awake()
    {
        AdjustAPCommand.adjustAPEvent.AddListener(UpdateTiles);
        Game.setActivePlayerEvent.AddListener(player => {
            activePlayer = player; 
            actionPoints = player.actionPoints;
            UpdateTiles(); 
        });
    }

    void AddTile(ActionPoint ap)
    {
        Debug.Log($"AddTile {ap.baseValue} {ap.actionType} {ap.actionTier} {ap.conditionals}");
        string name = ap.name;
        if (!APtiles.ContainsKey(name))
            APtiles.Add(name, Instantiate(actionPointPrefab, actionTiers[ap.actionTier].transform).GetComponent<UI_ActionPoint>());

        APtiles[name].SetTile(ap);
    }

    void RemoveTile(ActionPoint ap)
    {
        Debug.Log($"RemoveTile {ap.baseValue} {ap.actionType} {ap.actionTier} {ap.conditionals}");
        string name = ap.name;
        Destroy(APtiles[name].gameObject);
        APtiles.Remove(name); 
    }

    public void UpdateTiles()
    {
        // First cycle through our existing APTile keys and remove any uncessary ones
        foreach (string key in APtiles.Keys)
        {
            Debug.Log($"Checking to remove {key} ({actionPoints.All(ap => ap.name != key)})");
            if (actionPoints.All(ap => ap.name != key))
            {
                RemoveTile(APtiles[key].actionPoint);
            }
        }

        // Then cycle through our AP's and add tiles that we need
        foreach (ActionPoint actionPoint in actionPoints)
        {
            Debug.Log($"Checking to add {actionPoint.baseValue} {actionPoint.actionType} {actionPoint.actionTier} Action Points");

            string name = actionPoint.name;
            if (!APtiles.Keys.Contains(name))
            {
                AddTile(actionPoint);
            }    
        }
    }

}