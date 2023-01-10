using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class AdjustVPAction : PlayerAction
    {
        [SerializeField] int amount; 
        
        protected override Task Do()
        {
            Commands.Push(new AdjustVPCommand(Player.Faction, amount));
            return Task.CompletedTask; 
        }
    }
}
