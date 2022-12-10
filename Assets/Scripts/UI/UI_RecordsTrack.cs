using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace ImperialStruggle
{
    public class UI_RecordsTrack : MonoBehaviour
    {
        [SerializeField] Image vpBackground;
        [SerializeField] TextMeshProUGUI vpScore, ukDebt, franceDebt, ukTP, franceTP;

        private void Awake()
        {
            RecordsTrack.adjustDebtEvent += SetDebt;
            RecordsTrack.adjustDebtLimitEvent += SetDebt;
            RecordsTrack.adjustVPEvent += SetVPPoints;
            RecordsTrack.adjustTPEvent += SetTreatyPoints;
        }

        void SetVPPoints()
        {
            GraphicSettings graphicSettings = FindObjectOfType<Game>().graphicSettings;
            vpScore.text = RecordsTrack.VictoryPoints.ToString();

            if (RecordsTrack.VictoryPoints > 15)
                vpBackground.color = Game.Britain.Color;
            else if (RecordsTrack.VictoryPoints < 15)
                vpBackground.color = Game.France.Color;
            else
                vpBackground.color = Game.Neutral.Color;
        }

        void SetTreatyPoints()
        {
            ukTP.text = RecordsTrack.treatyPoints[Game.Britain].ToString();
            franceTP.text = RecordsTrack.treatyPoints[Game.France].ToString();
        }

        void SetDebt()
        {
            ukDebt.text = $"{RecordsTrack.currentDebt[Game.Britain]}-{RecordsTrack.debtLimit[Game.Britain]}";
            franceDebt.text = $"{RecordsTrack.currentDebt[Game.France]}-{RecordsTrack.debtLimit[Game.France]}";
        }
    }
}