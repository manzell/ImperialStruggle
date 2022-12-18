using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;

namespace ImperialStruggle
{
    public abstract class PlayerAction : GameAction
    {
        public Player Player { get; private set; }

        public void Setup(Player player)
        {
            this.Player = player;
            Setup(); 
        }

        public virtual void Setup() { }
    }
}