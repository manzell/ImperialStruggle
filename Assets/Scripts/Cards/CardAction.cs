using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public abstract class CardAction : SerializedScriptableObject
    {
        protected abstract void Do(Player actingPlayer);
        public virtual bool Can(Player actingPlayer) => true; 

        public void Execute(Player actingPlayer)
        {
            if (Can(actingPlayer))
                Do(actingPlayer); 
        }
    }
}
