using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 
using System;
using TMPro; 

public class UI_ActionPoints : MonoBehaviour
{
    [SerializeField] GameObject actionPointPrefab, minorAPs, majorAPs;
    [SerializeField] Button takeDebtButton, activateTPbutton, reduceDebtButton; 
    Dictionary<(Game.ActionType, Game.ActionTier), UI_ActionPoint> APtiles = new Dictionary<(Game.ActionType, Game.ActionTier), UI_ActionPoint>();
    RecordsTrack recordsTrack;

    private void Awake()
    {
        recordsTrack = FindObjectOfType<RecordsTrack>();
        UI_PlayerBoard.setFactionEvent.AddListener(faction => SetActionTiles(faction));
        AdjustActionPoints.adjustActionPointsEvent.AddListener(aap => SetActionTiles(aap.actingFaction));
        TakeDebt.takeDebtEvent.AddListener(td => SetActionTiles(td.actingFaction));
        ActivateTreatyPoint.activateTreatyPointsEvent.AddListener(atp => SetActionTiles(atp.actingFaction));
        SelectInvestmentTile.selectInvestmentTileEvent.AddListener(phase => reduceDebtButton.interactable = true); // TODO Update move this out to a different system
        PlayCard.playCardEvent.AddListener(pce => reduceDebtButton.interactable = false);
        AdjustActionPoints.adjustActionPointsEvent.AddListener(cap => reduceDebtButton.interactable = false); 

        SetButtons(UI_PlayerBoard.faction); 
    }

    void SetActionTiles(Game.Faction faction)
    {
        // Go through our list of Major Action Points - we either update the value or remove it if it's below zero
        Player player = Player.players[faction];

        SetButtons(faction);

        foreach (Game.ActionType actionType in Enum.GetValues(typeof(Game.ActionType)))
        {
            if (player.actionPoints.ContainsKey(actionType) && player.majorActionPoints[actionType] > 0)
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

    void SetButtons(Game.Faction faction)
    {
        Color color = FindObjectOfType<Game>().graphicSettings.factionColors[faction];

        if(Phase.currentPhase is ActionRound && (Phase.currentPhase as ActionRound).actingFaction == faction)
        {
            takeDebtButton.interactable = recordsTrack.availableDebt[faction] > 0;
            takeDebtButton.GetComponentInChildren<TextMeshProUGUI>().color = color;

            activateTPbutton.interactable = recordsTrack.treatyPoints[faction] > 0;
            activateTPbutton.GetComponentInChildren<TextMeshProUGUI>().color = color;

            reduceDebtButton.interactable &= recordsTrack.currentDebt[faction] > 0; 

            if(reduceDebtButton.interactable) 
                reduceDebtButton.GetComponentInChildren<TextMeshProUGUI>().color = color;
        }
        else
        {
            takeDebtButton.interactable = false; 
            takeDebtButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;

            activateTPbutton.interactable = false;
            activateTPbutton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;

            reduceDebtButton.interactable = false;
            reduceDebtButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
    }

    public void TakeDebtResponse()
    {
        Game.Faction faction = (Phase.currentPhase as ActionRound).actingFaction; 
        Phase.currentPhase.gameActions.Add(new TakeDebt(faction, 1)); 
    }

    public void ActiveTPresponse()
    {
        Game.Faction faction = (Phase.currentPhase as ActionRound).actingFaction;
        Phase.currentPhase.gameActions.Add(new ActivateTreatyPoint(faction, 1));
    }

    public void ReduceDebtResponse()
    {
        ActionRound actionRound = Phase.currentPhase as ActionRound;
        Game.Faction faction = actionRound.actingFaction;

        actionRound.gameActions.Add(new AdjustDebt(faction, -2)); 
    }
}
