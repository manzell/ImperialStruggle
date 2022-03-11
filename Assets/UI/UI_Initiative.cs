using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UI_Initiative : MonoBehaviour
{
    [SerializeField] GameObject initiativeWindow; 
    [SerializeField] Button playButton, passButton;
    [SerializeField] Image actingFlag;

    public void Awake()
    {
        InitiativePhase.InitiativePhaseStart.AddListener(Open);
        InitiativePhase.InitiativePhaseEnd.AddListener(Close);
    }

    public void Open(InitiativePhase phase)
    {
        actingFlag.sprite = FindObjectOfType<Game>().graphicSettings.flags[phase.initiative]; 

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => phase.Select(InitiativePhase.Response.Play));

        passButton.onClick.RemoveAllListeners();
        passButton.onClick.AddListener(() => phase.Select(InitiativePhase.Response.Pass));

        initiativeWindow.SetActive(true);
    }

    public void Close(InitiativePhase phase)
    {
        initiativeWindow.SetActive(false); 
    }
}
