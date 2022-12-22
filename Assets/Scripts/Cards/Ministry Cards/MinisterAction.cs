using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    [System.Serializable]
    public abstract class MinisterAction : IPlayerAction
    {
        [field: SerializeField] public string Name { get; private set; }
        public Player Player { get; private set; } 
        protected bool Exhausted;
        public Stack<Command> Commands { get; private set; }

        public virtual bool Eligible(Space space) => false;
        protected virtual bool Can(Player player) => !Exhausted;
        public bool Can() => Can(Player);
        public virtual Task Do(Player player) => Task.CompletedTask;
        public virtual void Reveal(Player player) { }
        protected virtual void Retire(Player player) { }

        protected void Reset(Phase phase)
        {
            if (phase is PeaceTurn)
                Exhausted = false;
        }

        public virtual void Setup(Player player)
        {
            this.Player = player; 
            Phase.PhaseStartEvent += Reset;
        }

    }
}
