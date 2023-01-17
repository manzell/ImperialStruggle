using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    // Returns True if the Space can be Shiftted
    public class ShiftSpaceCondition : Conditional<Space>
    {
        public enum ShiftType { Any, Unflag, Flag }
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
}
