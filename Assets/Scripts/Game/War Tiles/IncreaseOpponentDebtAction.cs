using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class IncreaseOpponentDebtAction : PlayerAction
    {
        [SerializeField] int amount = 1; 

        protected override void Do()
        {
            Commands.Push(new AdjustDebtCommand(player.Opponent.faction, amount)); 
        }
    }
}
