using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class ShiftSpace : Command
{
    public static UnityEvent<ShiftSpace> shiftSpaceEvent = new UnityEvent<ShiftSpace>();
    public Calculation<List<Space>> eligibleSpaces;
    public Space space;
    Game.Faction prevFlag;

    public override void Do(BaseAction action)
    {
        //prevFlag = space.flag;

        //Debug.Log($"{actingFaction} {(space.flag == Game.Faction.Neutral ? "Flags" : "Unflags")} {space}");

        //if (space.flag == Game.Faction.Neutral)
        //    space.flag = actingFaction;
        //else if (space.flag != actingFaction)
        //    space.flag = Game.Faction.Neutral;

        //shiftSpaceEvent.Invoke(this); 
    }

    public override void Undo() => space.flag = prevFlag;
}
