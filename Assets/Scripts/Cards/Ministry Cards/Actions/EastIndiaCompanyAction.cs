using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq; 

namespace ImperialStruggle
{
    public class EastIndiaCompanyAction : MinisterAction
    {
        [SerializeField] List<AdvantageTile> tiles; 

        public override Task Do(Player player)
        {
            ScoreGlobalDemandAction.ScoreGlobalDemandEvent += Bonus;
            return Task.CompletedTask; 
        }

        void Bonus()
        {
            Commands.Push(new AdjustVPCommand(Player.Faction, tiles.Count(tile => tile.faction == Player.Faction && !tile.Exhausted)));
        }
    }
}
