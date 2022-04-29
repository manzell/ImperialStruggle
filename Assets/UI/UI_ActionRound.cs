using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UI_ActionRound : MonoBehaviour
{
    [SerializeField] Button endARButton;

    private void Awake()
    {
        Phase.phaseEndEvent.AddListener(phase => endARButton.gameObject.SetActive(false));
    }

    void OnActionRoundStart(ActionRound ar)
    {
        endARButton.gameObject.SetActive(true);
        endARButton.onClick.RemoveAllListeners();
        //endARButton.onClick.AddListener(() => ar.callback.Invoke()); 
    }
}
