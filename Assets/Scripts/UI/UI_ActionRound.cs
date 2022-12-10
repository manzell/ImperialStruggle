using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ImperialStruggle
{
    public class UI_ActionRound : MonoBehaviour
    {
        [SerializeField] Button endARButton;

        private void Awake()
        {
            Phase.PhaseEndEvent += phase => endARButton.gameObject.SetActive(false);
            Phase.PhaseStartEvent += phase => { if (phase.TryGetComponent(out ActionRound ar)) OnActionRoundStart(ar); };
        }

        void OnActionRoundStart(ActionRound ar)
        {
            endARButton.gameObject.SetActive(true);
            endARButton.onClick.RemoveAllListeners();
            //endARButton.onClick.AddListener(() => ar.GetComponent<Phase>().callback.Invoke()); 
        }
    }
}