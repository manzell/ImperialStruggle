using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector; 
using System.Linq;
using ImperialStruggle;

public class SelectMinistryCardAction : GameAction
{
    protected override void Do()
    {
        PeaceTurn peaceTurn = Phase.CurrentPhase.GetComponentInParent<PeaceTurn>();

        if (peaceTurn != null)
            foreach(Player player in Player.players)
                new Selection<MinistryCard>(player, player.ministers.Where(minister => minister.data.eras.Contains(peaceTurn.era)), Finish);
    }

    void Finish(Selection<MinistryCard> selectedMinisters)
    {
        foreach(MinistryCard minister in selectedMinisters)
            commands.Add(new SelectMinistryCardCommand(minister)); 
    }
}