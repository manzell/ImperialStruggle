using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    [System.Serializable]
    public abstract class MinisterAction : PlayerAction, IAction
    {
        protected bool Exhausted;

        public override bool Can(Player player) => !Exhausted && base.Can(player); 

        public virtual void Reveal(Player player) { }
        protected virtual void Retire(Player player) { }

        public override async Task Execute(IAction context)
        {
            Exhausted = true;
            PeaceTurn.StartPeaceTurnEvent += Reset;
            await base.Execute(context ?? this);
        }

        protected void Reset(Phase phase)
        {
            if (phase is PeaceTurn)
            {
                Exhausted = false;
                PeaceTurn.StartPeaceTurnEvent -= Reset; 
            }
        }
    }
}
