using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    // Returns True if the Space can be Shiftted
    public class ShiftSpaceCondition : Conditional
    {
        public enum ShiftType { Any, Unflag, Flag }

        [SerializeField] ShiftType shiftType;
        [SerializeField] Space.SpaceType spaceType;

        public override bool Test(IPlayerAction context)
        {            
            if(context is TargetSpaceAction<Space> action && action.Space is FlaggableSpace space)
            {
                bool retval = false; 

                switch(shiftType)
                {
                    case ShiftType.Unflag:
                        retval = space.Flag != Game.Neutral && space.Flag != context.Player;
                        break;
                    case ShiftType.Flag:
                        retval = space.Flag == Game.Neutral;
                        break; 
                    case ShiftType.Any:
                        retval = space.Flag != context.Player;
                        break;
                }

                if (spaceType == Space.SpaceType.Market)
                    retval &= space is Market;
                else if (spaceType == Space.SpaceType.Political)
                    retval &= space is PoliticalSpace;
                else if (spaceType == Space.SpaceType.Naval)
                    retval &= space is NavalSpace;
                else if (spaceType == Space.SpaceType.Fort)
                    retval &= space is Fort;
                else if (spaceType == Space.SpaceType.Territory)
                    retval &= space is Territory;

                return retval;
            }

            return false; 
        }
    }
}
