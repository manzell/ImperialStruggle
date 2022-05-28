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
        Phase.phaseStartEvent.AddListener(phase => { if (phase.TryGetComponent(out ActionRound ar)) OnActionRoundStart(ar); });
    }

    void OnActionRoundStart(ActionRound ar)
    {
        endARButton.gameObject.SetActive(true);
        endARButton.onClick.RemoveAllListeners();
        endARButton.onClick.AddListener(() => ar.GetComponent<Phase>().callback.Invoke()); 
    }
}
