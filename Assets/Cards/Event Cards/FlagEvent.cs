using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagEvent : CardEvent
{
    [SerializeField] List<PoliticalSpace> eligibleSpaces;
    [SerializeField] int numSpaces = 1;

    public override void Event()
    {
        Debug.Log($"Flag {numSpaces} of {eligibleSpaces.Count} spaces");

        eligibleSpaces.ForEach(space =>
        {
            ShiftPoliticalSpace shift = space.gameObject.AddComponent<ShiftPoliticalSpace>();
            shift.requiredShiftType = ShiftPoliticalSpace.ActionType.Flag;
            shift.requiredFaction = Game.Faction.France; // yahh fix this!
            shift.fixedActionCost.Clear();
            shift.requiredActionType = Game.ActionType.None;

            Phase.phaseEndEvent.AddListener(phase => {
                Destroy(shift);
            }); // It's possible this fails if we DO the event before the PhaseEnd. We shall see

            shift.DoEvent.AddListener(shift => Destroy(shift));
        });
    }
}
