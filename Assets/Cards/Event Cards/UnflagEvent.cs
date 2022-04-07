using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnflagEvent : CardEvent
{
    [SerializeField] List<PoliticalSpace> eligibleSpaces;
    [SerializeField] int numSpaces = 1; 

    public override void Event()
    {
        Debug.Log("Unflag a Political space in Europe (not Spain or Austria).");
        // Unflag an enemy political space that isn't in Spain or Austria

        eligibleSpaces.ForEach(space =>
        {
            ShiftPoliticalSpace shift = space.gameObject.AddComponent<ShiftPoliticalSpace>();
            shift.requiredShiftType = ShiftPoliticalSpace.ActionType.Deflag;
            shift.requiredFaction = Game.Faction.France;
            shift.fixedActionCost.Clear();
            shift.requiredActionType = Game.ActionType.None;

            Phase.phaseEndEvent.AddListener(phase => {
                Destroy(shift);
            }); // It's possible this fails if we DO the event before the PhaseEnd. We shall see

            shift.DoEvent.AddListener(shift => Destroy(shift));
        });
    }
}
