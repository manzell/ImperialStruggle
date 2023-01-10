using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class ShiftSpaceAdvantageAction : PlayerAction, PurchaseAction
    {
        enum ShiftType { Shift, Flag, Unflag }
        [SerializeField] HashSet<FlaggableSpace> eligibleSpaces; 
        [field: SerializeField] public ActionPoint ActionCost { get; private set; }

        protected override async Task Do()
        {
            Selection<FlaggableSpace> selection = new(Player, eligibleSpaces);
            await selection.Completion; 

            if(selection.Count() > 0)
            {
                if (selection.First().Flag == Game.Neutral)
                    Commands.Push(new FlagSpaceCommand(selection.First(), Player.Faction));
                if (selection.First().Flag == Player.Opponent.Faction)
                    Commands.Push(new UnflagCommand(selection.First()));
            }
        }
    }
}
