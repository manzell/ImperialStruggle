using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class IncreaseOpponentDebtAction : PlayerAction
    {
        [SerializeField] int amount = 1; 

        protected override Task Do()
        {
            Commands.Push(new AdjustDebtCommand(Player.Opponent.Faction, amount));
            return Task.CompletedTask; 
        }
    }
}
