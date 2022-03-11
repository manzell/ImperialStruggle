using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class UI_ActionPoints : MonoBehaviour
{
    [SerializeField] GameObject actionPointPrefab, minorAPs, majorAPs; 
    Dictionary<Game.ActionType, UI_ActionPoint> majorAPtiles = new Dictionary<Game.ActionType, UI_ActionPoint>(),
        minorAPtiles = new Dictionary<Game.ActionType, UI_ActionPoint>();

    private void Awake()
    {
        UI_PlayerBoard.setFactionEvent.AddListener(faction => SetActionTiles(faction));
        AdjustActionPoints.adjustActionPointsEvent.AddListener(aap => { Debug.Log("Adjust Action Points!"); SetActionTiles(FindObjectOfType<UI_PlayerBoard>().faction); });
    }

    void SetActionTiles(Game.Faction faction)
    {
        // Go through our list of Major Action Points - we either update the value or remove it if it's below zero
        Player player = Player.players[faction];

        foreach (Game.ActionType actionType in Enum.GetValues(typeof(Game.ActionType)))
        {
            if (player.majorActionPoints.ContainsKey(actionType) && player.majorActionPoints[actionType] > 0)
            {
                if (!majorAPtiles.ContainsKey(actionType))
                {
                    GameObject action = Instantiate(actionPointPrefab, majorAPs.transform);
                    majorAPtiles.Add(actionType, action.GetComponent<UI_ActionPoint>());
                }

                majorAPtiles[actionType].SetDisplay(actionType, player.majorActionPoints[actionType]);
            }
            else if(majorAPtiles.ContainsKey(actionType))
            {
                Destroy(majorAPtiles[actionType].gameObject);
                majorAPtiles.Remove(actionType);
            }

            if (player.minorActionPoints.ContainsKey(actionType) && player.minorActionPoints[actionType] > 0)
            {
                if (!minorAPtiles.ContainsKey(actionType))
                {
                    GameObject action = Instantiate(actionPointPrefab, minorAPs.transform);
                    minorAPtiles.Add(actionType, action.GetComponent<UI_ActionPoint>());
                }

                minorAPtiles[actionType].SetDisplay(actionType, player.minorActionPoints[actionType]);
            }
            else if (minorAPtiles.ContainsKey(actionType))
            {
                Destroy(minorAPtiles[actionType].gameObject);
                minorAPtiles.Remove(actionType);
            }
        }
    }
}
