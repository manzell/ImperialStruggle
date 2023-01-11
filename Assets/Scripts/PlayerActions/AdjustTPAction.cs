using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class AdjustTPAction : PlayerAction
    {
        [SerializeField] int tpAdjustAmount;
        protected override Task Do()
        {
            Commands.Push(new AdjustTreatyPointsCommand(Player.Faction, tpAdjustAmount));
            return Task.CompletedTask;
        }
    }
}
