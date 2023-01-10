using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class AddAPAction : PlayerAction
    {
        [SerializeField] ActionPoint actionPoint; 
        protected override Task Do()
        {
            Commands.Push(new AddAPCommand(Player, actionPoint));
            return Task.CompletedTask; 
        }
    }
}
