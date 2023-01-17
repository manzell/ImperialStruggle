using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class JohnLawAction : MinisterAction
    {
        [SerializeField] List<PoliticalData> scotlandSpaces = new();
        System.Action<PeaceTurn> reduceDebt; 

        public override void Reveal(Player player)
        {
            reduceDebt = turn => ReduceDebt(player); 
            PeaceTurn.EndPeaceTurnEvent += reduceDebt; 
        }

        protected override void Retire(Player player)
        {
            PeaceTurn.EndPeaceTurnEvent -= reduceDebt;
        }

        void ReduceDebt(Player player)
        {
            int debtReduction = scotlandSpaces.Any(data => Game.SpaceLookup[data].Control == player.Faction) ? 2 : 1;
            Commands.Push(new AdjustDebtCommand(player.Faction, -debtReduction)); 
        }
    }
}