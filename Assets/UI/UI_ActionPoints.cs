using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 
using System;
using TMPro;
using Sirenix.OdinInspector; 

public class UI_ActionPoints : SerializedMonoBehaviour
{
    [SerializeField] GameObject actionPointPrefab;
    [SerializeField] Dictionary<Game.ActionTier, GameObject> actionTiers = new Dictionary<Game.ActionTier, GameObject>(); 
    [SerializeField] Button takeDebtButton, activateTPbutton, reduceDebtButton; 
    Dictionary<(Game.ActionType, Game.ActionTier), UI_ActionPoint> APtiles = new Dictionary<(Game.ActionType, Game.ActionTier), UI_ActionPoint>();
    RecordsTrack recordsTrack;

    private void Awake()
    {
        recordsTrack = FindObjectOfType<RecordsTrack>();
        UI_PlayerBoard.setFactionEvent.AddListener(faction => SetActionTiles(faction));
        AdjustAPCommand.adjustActionPointsEvent.AddListener(aap => SetActionTiles(aap.targetFaction));
        //TakeDebt.takeDebtEvent.AddListener(td => SetActionTiles(td.targetFaction));
        //ActivateTreatyPoint.activateTreatyPointsEvent.AddListener(atp => SetActionTiles(atp.targetFaction));
        PlayCard.playCardEvent.AddListener(pce => reduceDebtButton.interactable = false);
        AdjustAPCommand.adjustActionPointsEvent.AddListener(cap => reduceDebtButton.interactable = false); 

        SetButtons(UI_PlayerBoard.faction); 
    }

    void SetActionTiles(Game.Faction faction)
    {
        // Go through our list of Major Action Points - we either update the value or remove it if it's below zero
        Player player = Player.players[faction];

        SetButtons(faction);
        /*
        foreach (Game.ActionType type in Enum.GetValues(typeof(Game.ActionType)))
        {
            foreach (Game.ActionTier tier in Enum.GetValues(typeof(Game.ActionTier)))
            {
                if(player.actionPoints.TryGetValue((type, tier), out Calculation <int> ap))
                {
                    if(ap.value > 0)
                    {
                        if (!APtiles.ContainsKey((type, tier)))
                        {
                            GameObject action = Instantiate(actionPointPrefab, actionTiers[tier].transform);
                            APtiles.Add((type, tier), action.GetComponent<UI_ActionPoint>());
                        }

                        APtiles[(type, tier)].SetDisplay(type, ap.value);
                    }
                    else if(APtiles.ContainsKey((type, tier)))
                        Destruct(type, tier);
                }
                else if(APtiles.ContainsKey((type, tier)))
                    Destruct(type, tier); 

                void Destruct(Game.ActionType type, Game.ActionTier tier)
                {
                    Destroy(APtiles[(type, tier)].gameObject);
                    APtiles.Remove((type, tier));
                }
            }
        }
        */
    }

    void SetButtons(Game.Faction faction)
    {
        //Color color = FindObjectOfType<Game>().graphicSettings.factionColors[faction];

        //if(Phase.currentPhase is ActionRound && (Phase.currentPhase as ActionRound).actingFaction == faction)
        //{
        //    takeDebtButton.interactable = recordsTrack.availableDebt[faction] > 0;
        //    takeDebtButton.GetComponentInChildren<TextMeshProUGUI>().color = color;

        //    activateTPbutton.interactable = recordsTrack.treatyPoints[faction] > 0;
        //    activateTPbutton.GetComponentInChildren<TextMeshProUGUI>().color = color;

        //    reduceDebtButton.interactable &= recordsTrack.currentDebt[faction] > 0; 

        //    if(reduceDebtButton.interactable) 
        //        reduceDebtButton.GetComponentInChildren<TextMeshProUGUI>().color = color;
        //}
        //else
        //{
        //    takeDebtButton.interactable = false; 
        //    takeDebtButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;

        //    activateTPbutton.interactable = false;
        //    activateTPbutton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;

        //    reduceDebtButton.interactable = false;
        //    reduceDebtButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        //}
    }


    public void ReduceDebtResponse()
    {
        //ActionRound actionRound = Phase.currentPhase as ActionRound;
        //Game.Faction faction = actionRound.actingFaction;

        //AdjustDebtCommand adjustDebt = new AdjustDebtCommand();
        //adjustDebt.targetFaction = faction;
        //adjustDebt.adjustAmt = -2;
        
    }
}