using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class FlagFortCommand : Command
    {
        Faction faction;
        Fort fort;
        public FlagFortCommand(Faction faction, Fort fort)
        {
            this.faction = faction;
            this.fort = fort;
        }

        public override void Do(GameAction action)
        {
            fort.SetFlag(faction);
            Debug.Log($"{faction} flags {fort.name}");
        }
    }
}