using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ImperialStruggle
{
    public class ToggleActivePlayerBehavior : MonoBehaviour
    {
        private void Start()
        {
            Game.setActivePlayerEvent += SetActivePlayer;
        }

        void SetActivePlayer(Player player)
        {
            Game.ActivePlayer = player;
            FindObjectsOfType<UI_Player>().ForEach(ui => ui.gameObject.SetActive(ui.player == player));
        }

        public void Update()
        {
            if (Keyboard.current.tabKey.wasPressedThisFrame)
                Game.setActivePlayerEvent(Game.ActivePlayer.Opponent);
        }
    }
}