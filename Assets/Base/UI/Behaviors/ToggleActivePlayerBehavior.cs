using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class ToggleActivePlayerBehavior : MonoBehaviour
{
    public void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
            Game.SetActivePlayer(Player.players[Game.activePlayer.faction.Opposition()]);
    }
}
