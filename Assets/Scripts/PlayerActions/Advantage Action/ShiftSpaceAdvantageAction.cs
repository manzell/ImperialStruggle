using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class ShiftSpaceAdvantageAction : PlayerAction, _PurchaseAction
    {
        enum ShiftType { Any, Flag, Unflag }
        [SerializeField] Calculation<IEnumerable<ISelectable>> eligibleSpaces;
        [SerializeField] ShiftType shiftType; 
        [field: SerializeField] public ActionPoint ActionCost { get; private set; }

        protected override async Task Do(IAction context)
        {
            Selection<ISelectable> selection = new(Player, eligibleSpaces.Calculate(this)); 
            await selection.Completion; 

            if(selection.FirstOrDefault() is FlaggableSpace space)
            {
                if (space.Flag == Game.Neutral && shiftType != ShiftType.Unflag)
                    Commands.Push(new FlagSpaceCommand(space, Player.Faction));
                else if (space.Flag != Game.Neutral && shiftType != ShiftType.Flag)
                    Commands.Push(new UnflagCommand(space));
            }
        }
    }

    public class ShiftSpaceResponse : SelectionReceiver<ISelectable>
    {
        enum ShiftType { Any, Flag, Unflag }
        [SerializeField] ShiftType type; 

        public override void OnSelect(Selection<ISelectable> selection)
        {
            foreach (FlaggableSpace space in selection)
            {
                if (type == ShiftType.Any || (type == ShiftType.Flag && space.Flag == Game.Neutral) || (type == ShiftType.Unflag && space.Flag != Game.Neutral))
                    Commands.Push(new ShiftSpaceCommand(space, selection.player.Faction));
            }
        }
    }
}
