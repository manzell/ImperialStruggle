using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public enum ShiftType { Any, Unflag, Flag }
    // Returns True if the Space can be Shiftted
    public class ShiftableSpace : Conditional<Space>
    {
        [SerializeField] ShiftType shiftType;
        [SerializeField] Faction faction; 

        protected override bool Test(Space space)
        {
            bool retval = false; 
            switch(shiftType)
            {
                case ShiftType.Unflag:
                    retval = space.Flag != Game.Neutral && space.Flag !=faction;
                    break;
                case ShiftType.Flag:
                    retval = space.Flag == Game.Neutral;
                    break; 
                case ShiftType.Any:
                    retval = space.Flag != faction;
                    break;
            }

            return retval;
        }
    }

    public class ShiftTypeCondition : Conditional<IAction>
    {
        [SerializeField] ShiftType shiftType;

        protected override bool Test(IAction context)
        {
            if (context is ShiftMarketAction shiftMarket)
                return shiftMarket.shiftType == ShiftType.Unflag;
            if (context is ShiftPoliticalSpaceAction shiftPolitical)
                return shiftPolitical.shiftType == ShiftType.Unflag;
            else
                return false;
        }
    }
}
