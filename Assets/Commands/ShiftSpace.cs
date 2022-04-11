using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class ShiftSpace : Command
{
    public static UnityEvent<ShiftSpace> shiftSpaceEvent = new UnityEvent<ShiftSpace>();
    public Space space;
    Game.Faction prevFlag;

    public ShiftSpace(Space space, Game.Faction faction)
    {
        this.space = space;
        targetFaction = faction;
        Do(faction); 
    }

    public override void Do(Game.Faction faction)
    {
        prevFlag = space.flag;

        Debug.Log($"{faction} {(space.flag == Game.Faction.Neutral ? "Flags" : "Unflags")} {space}");

        if (space.flag == Game.Faction.Neutral)
            space.flag = faction;
        else if (space.flag != faction)
            space.flag = Game.Faction.Neutral;

        shiftSpaceEvent.Invoke(this); 
    }

    public override void Undo() => space.flag = prevFlag;
}
