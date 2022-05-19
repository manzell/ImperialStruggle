using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActivePlayer : MonoBehaviour
{
    public KeyCode setActivePlayerKeycode = KeyCode.Tab; 
    public void Update()
    {
        if (Input.GetKeyDown(setActivePlayerKeycode))
            Game.SetActivePlayer(Player.players[Game.activePlayer.faction.Opposition()]);
    }
}
