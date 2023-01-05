using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    [System.Serializable]
    public abstract class MinisterAction : PlayerAction, IPlayerAction
    {
        protected bool Exhausted;

        public override bool Can() => !Exhausted;

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
