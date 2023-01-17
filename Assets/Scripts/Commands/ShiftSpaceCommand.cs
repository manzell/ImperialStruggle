using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class ShiftSpaceCommand : Command
    {
        Faction faction;
        FlaggableSpace space;
        public ShiftSpaceCommand(FlaggableSpace space, Faction faction)
        {
            this.faction = faction;
            this.space = space;
        }

        public override void Do(IAction context)
        {
            if(space.Flag == Game.Neutral)
                space.SetFlag(faction);
            else 
                space.SetFlag(Game.Neutral);

            Debug.Log($"{faction} shifts {space.Name} to {(space.Flag == faction ? "Control" : "Neutral")}");
        }
    }
}
