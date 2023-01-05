using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;

namespace ImperialStruggle
{
    public abstract class PlayerAction : GameAction, IPlayerAction
    {
        public Player Player { get; private set; }
        [field: SerializeField] public bool Passive { get; protected set; }

        public virtual void Setup(Player player)
        {
            this.Player = player;
        }

        public virtual bool Eligible(Space space) => false; 
    }
}