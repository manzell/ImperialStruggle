using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class ShiftEvent : CardEvent
{
    [SerializeField] List<PoliticalSpace> eligibleSpaces;
    [SerializeField] int numSpaces = 1;

    public override void Event() // TODO this is incomplete
    {
        eligibleSpaces.Where(space => space.flag != faction).ToList().ForEach(space =>
        {
            ShiftPoliticalSpace shift = space.gameObject.AddComponent<ShiftPoliticalSpace>();

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
