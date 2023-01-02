using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    [System.Serializable]
    public abstract class MinisterAction : PlayerAction, IPlayerAction
    {
        protected bool Exhausted;

        protected virtual bool Can() => !Exhausted;

        public virtual void Reveal() { }
        protected virtual void Retire() { }

        protected void Reset(Phase phase)
        {
            if (phase is PeaceTurn)
                Exhausted = false;
        }

        public override void Setup(Player player)
        {
            PeaceTurn.StartPeaceTurnEvent += Reset;
            base.Setup(player);
        }
    }
}
