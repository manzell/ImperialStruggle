using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class ActionRound : MonoBehaviour
{
    public Player actingPlayer;
    public InvestmentTile investmentTile;

    private void Awake()
    {
        Phase.phaseStartEvent.AddListener(OnActionRoundStart); 
    }

    void OnActionRoundStart(Phase phase)
    {
        if (phase == GetComponent<Phase>()) 
            Game.setActivePlayerEvent.Invoke(actingPlayer);
    }
}
