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

        public override void Reveal(Player player)
        {
            PeaceTurn peaceTurn = null; 

            if (Phase.CurrentPhase is PeaceTurn)
                peaceTurn = (PeaceTurn)Phase.CurrentPhase;
            else if (Phase.CurrentPhase is ActionRound ar)
                peaceTurn = ar.GetComponentInParent<PeaceTurn>();

            if (peaceTurn != null)
                PeaceTurn.EndPeaceTurnEvent += ReduceDebt; 
        }

        protected override Task Do()
        {
            throw new System.NotImplementedException();
        }

        void ReduceDebt(PeaceTurn peaceTurn)
        {
            int debtReduction = scotlandSpaces.Any(space => space.control == Player.Faction) ? 2 : 1;
            Commands.Push(new AdjustDebtCommand(Player.Faction, -debtReduction)); 
        }
    }
}