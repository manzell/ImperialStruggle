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

    private void Awake()
    {
        RecordsTrack.adjustDebtEvent.AddListener(SetDebt);
        RecordsTrack.adjustDebtLimitEvent.AddListener(SetDebt);
        RecordsTrack.adjustVPEvent.AddListener(SetVPPoints);
        RecordsTrack.adjustTPEvent.AddListener(SetTreatyPoints); 

    }

    void SetVPPoints()
    {
        GraphicSettings graphicSettings = FindObjectOfType<Game>().graphicSettings;
        vpScore.text = RecordsTrack.VictoryPoints.ToString();

        if (RecordsTrack.VictoryPoints > 15)
            vpBackground.color = graphicSettings.factionColors[Game.Faction.Britain];
        else if (RecordsTrack.VictoryPoints < 15)
            vpBackground.color = graphicSettings.factionColors[Game.Faction.France];
        else
            vpBackground.color = graphicSettings.factionColors[Game.Faction.Neutral];
    }

    void SetTreatyPoints()
    {
        ukTP.text = RecordsTrack.treatyPoints[Game.Faction.Britain].ToString();
        franceTP.text = RecordsTrack.treatyPoints[Game.Faction.France].ToString();
    }

    void SetDebt()
    {
        ukDebt.text = $"{RecordsTrack.currentDebt[Game.Faction.Britain]}-{RecordsTrack.debtLimit[Game.Faction.Britain]}";
        franceDebt.text = $"{RecordsTrack.currentDebt[Game.Faction.France]}-{RecordsTrack.debtLimit[Game.Faction.France]}";
    }
}