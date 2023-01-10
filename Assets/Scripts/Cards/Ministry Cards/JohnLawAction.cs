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
        [SerializeField] List<Space> scotlandSpaces = new List<Space>();

        public override void Reveal() => Do(); 

        protected override Task Do()
        {
            PeaceTurn.EndPeaceTurnEvent += ReduceDebt;
            return Task.CompletedTask; 
        }

        protected override void Retire()
        {
            PeaceTurn.EndPeaceTurnEvent -= ReduceDebt;
        }

        void ReduceDebt(PeaceTurn peaceTurn)
        {
            int debtReduction = scotlandSpaces.Any(space => space.Control == Player.Faction) ? 2 : 1;
            Commands.Push(new AdjustDebtCommand(Player.Faction, -debtReduction)); 
        }
    }
}