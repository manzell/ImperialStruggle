using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UI_ActionPoint : MonoBehaviour
{
    [SerializeField] Image apIcon;
    [SerializeField] TextMeshProUGUI ap; 

    public ActionPoint.ActionType actionType;
    public int actionPoints; 

    public void SetDisplay(ActionPoint.ActionType at, int amt)
    {
        GraphicSettings graphicSettings = FindObjectOfType<Game>().graphicSettings; 
        actionType = at; 
        apIcon.sprite = graphicSettings.actionIcons[at];
        actionPoints = amt;
        ap.text = actionPoints.ToString(); 
    }
}
