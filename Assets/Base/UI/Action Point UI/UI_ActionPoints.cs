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

    private void Awake()
    {
        AdjustAPCommand.adjustAPEvent.AddListener(UpdateTiles);
        Game.setActivePlayerEvent.AddListener(player => {
            actionPoints = player.actionPoints;
            UpdateTiles(); 
        });
    }

    void AddTile(ActionPoint ap)
    {
        string name = $"{ap.actionType}-{ap.actionTier}-{ap.conditionText}"; 

        if (!APtiles.ContainsKey(name))
            APtiles.Add(name, Instantiate(actionPointPrefab, actionTiers[ap.actionTier].transform).GetComponent<UI_ActionPoint>());

        APtiles[name].SetTile(ap);
    }

    void RemoveTile(ActionPoint ap)
    {
        string name = $"{ap.actionType}-{ap.actionTier}-{ap.conditionText}";
        Destroy(APtiles[name].gameObject);
        APtiles.Remove(name); 
    }

    public void UpdateTiles()
    {
        // First cycle through our existing APTile keys and remove any uncessary ones
        List<string> keysToRemove = new List<string>();
        foreach (string key in APtiles.Keys)
            if (actionPoints.All(ap => ap.name != key))
                keysToRemove.Add(key); 

        foreach(string key in keysToRemove)
            RemoveTile(APtiles[key].actionPoint);

        // Then cycle through our AP's and add tiles that we need
        foreach (ActionPoint actionPoint in actionPoints)
            if (!APtiles.Keys.Contains(actionPoint.name))
                AddTile(actionPoint);
    }

}