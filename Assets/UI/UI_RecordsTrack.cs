using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UI_RecordsTrack : MonoBehaviour
{
    [SerializeField] Image vpBackground;
    [SerializeField] TextMeshProUGUI vpScore, ukDebt, franceDebt, ukTP, franceTP;
    RecordsTrack recordsTrack; 

    private void Awake()
    {
        recordsTrack = FindObjectOfType<RecordsTrack>();
        AdjustVictoryPoints.adjustVPEvent.AddListener(avp => SetVPPoints());
        AdjustTreatyPoints.adjustTPevent.AddListener((x, y) => SetTreatyPoints());
        AdjustDebt.adjustDebtEvent.AddListener((x, y) => SetDebt());

        SetVPPoints();
        SetTreatyPoints();
        SetDebt();

    }

    void SetVPPoints()
    {
        GraphicSettings graphicSettings = FindObjectOfType<Game>().graphicSettings;
        vpScore.text = recordsTrack.VictoryPoints.ToString();

        if (recordsTrack.VictoryPoints > 15)
            vpBackground.color = graphicSettings.factionColors[Game.Faction.England];
        else if (recordsTrack.VictoryPoints < 15)
            vpBackground.color = graphicSettings.factionColors[Game.Faction.France];
        else
            vpBackground.color = graphicSettings.factionColors[Game.Faction.Neutral];
    }

    void SetTreatyPoints()
    {
        ukTP.text = recordsTrack.treatyPoints[Game.Faction.England].ToString();
        franceTP.text = recordsTrack.treatyPoints[Game.Faction.France].ToString();
    }

    void SetDebt()
    {
        ukDebt.text = $"{recordsTrack.currentDebt[Game.Faction.England]}-{recordsTrack.debtLimit[Game.Faction.England]}";
        franceDebt.text = $"{recordsTrack.currentDebt[Game.Faction.France]}-{recordsTrack.debtLimit[Game.Faction.France]}";
    }
}