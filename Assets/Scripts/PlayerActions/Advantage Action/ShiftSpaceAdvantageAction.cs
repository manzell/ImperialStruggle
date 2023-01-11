using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class ShiftSpaceAdvantageAction : PlayerAction, PurchaseAction
    {
        enum ShiftType { Any, Flag, Unflag }
        [SerializeField] Calculation<HashSet<Space>> eligibleSpaces;
        [SerializeField] ShiftType shiftType; 
        [field: SerializeField] public ActionPoint ActionCost { get; private set; }

        protected override async Task Do()
        {
            Selection<Space> selection = new(Player, eligibleSpaces.Calculate(Player));
            await selection.Completion; 

            if(selection.Count() > 0)
            {
                if (selection.First().Flag == Game.Neutral && shiftType != ShiftType.Unflag)
                    Commands.Push(new FlagSpaceCommand(selection.First() as FlaggableSpace, Player.Faction));
                else if (selection.First().Flag != Game.Neutral && shiftType != ShiftType.Flag)
                    Commands.Push(new UnflagCommand(selection.First() as FlaggableSpace));
            }
        }
    }
}
