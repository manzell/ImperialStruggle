using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks; 

// To Take Debt, we must have 1 Available Debt, We Increase our Debt by 1 and add a Debt Action Point? 
namespace ImperialStruggle
{
    public class TakeDebtAction : PlayerAction
    {
        public override bool Can() => base.Can() && RecordsTrack.availableDebt[Player.Faction] > 0; 

        protected override Task Do()
        {
            Commands.Push(new AdjustDebtCommand(Player.Faction, 1));
            return Task.CompletedTask; 
        }
    }
}