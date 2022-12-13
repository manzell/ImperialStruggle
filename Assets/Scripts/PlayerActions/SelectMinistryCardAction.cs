using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ImperialStruggle;
using Sirenix.Utilities;

namespace ImperialStruggle
{
    public class SelectMinistryCardAction : GameAction
    {
        protected override void Do()
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
                foreach (Player player in Player.players)
                    new Selection<MinistryCardData>(player, player.ministers.Keys.Where(minister => minister.eras.Contains(peaceTurn.era)), 
                    selection => selection.ForEach(minister => Commands.Push(new SelectMinistryCardCommand(minister))));

            // TODO - Collect BOTH tasks and await them as a group
        }
    }
}