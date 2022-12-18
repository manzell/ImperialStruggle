using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class FlagSpaceCommand : Command
    {
        Faction faction;
        FlaggableSpace space; 
        public FlagSpaceCommand(FlaggableSpace space, Faction faction)
        {
            this.faction = faction;
            this.space = space;
        }

        public override void Do(GameAction action)
        {
            space.SetFlag(faction);
            Debug.Log($"{faction} flags {space.Name}");
        }
    }
}