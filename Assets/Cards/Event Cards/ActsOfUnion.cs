using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class ActsOfUnion : EventCard
{
    [SerializeField] List<Space> prestigeSpaces = new List<Space>();
    [SerializeField] List<Space> unflaggableSpaces = new List<Space>();
    [SerializeField] Map europeMap; 

    public override void PlayCard(ActionRound actionRound)
    {
        bool bonus = BonusCondition(actionRound.actingFaction); 

        switch(actionRound.actingFaction)
        {
            case Game.Faction.England:
                BritainBaseEvent();
                if (bonus) BritainBonus();
                break;
            case Game.Faction.France:
                FranceBaseEvent();
                if (bonus) FranceBonus();
                break; 
        }

        callback.Invoke(); 
    }

    public bool BonusCondition(Game.Faction faction)
    {
        Game.Faction opposingFaction = faction == Game.Faction.England ? Game.Faction.France : Game.Faction.England;

        return prestigeSpaces.Where(space => space.flag == faction && space.conflictMarker == false).Count() >
            prestigeSpaces.Where(space => space.flag == opposingFaction && space.conflictMarker == false).Count();
    }

    public void BritainBaseEvent()
    {
        Debug.Log("Unified Parliament reduces Scottish intrigue in Europe: 1 DP (unflagging in Europe only).");

        // We're going to create an Unflag Event for Britain and add it to the space's Game Object. 
        FindObjectsOfType<PoliticalSpace>().Where(space => space.map == europeMap).ToList().ForEach(space =>
        {
            ShiftPoliticalSpace shift = space.gameObject.AddComponent<ShiftPoliticalSpace>(); 
            shift.requiredShiftType = ShiftPoliticalSpace.ActionType.Deflag;
            shift.requiredFaction = Game.Faction.England; 
            shift.actionCost = -1;

            Phase.phaseEndEvent.AddListener(phase => {                
                Destroy(shift); 
            }); // It's possible this fails if we DO the event before the PhaseEnd. We shall see

            shift.DoEvent.AddListener(shift => Destroy(shift));
        }); 
    }

    public void BritainBonus()
    {
        // 2 VP for Britain
        Phase.currentPhase.gameActions.Add(new AdjustVictoryPoints(Game.Faction.England, 2)); 
    }

    public void FranceBaseEvent()
    {
        Debug.Log("Habsburgs Isolated"); 
        Phase.currentPhase.gameActions.Add(new AdjustActionPoints(Game.Faction.France, new Dictionary<(Game.ActionType, Game.ActionTier), int> { 
            { (Game.ActionType.Diplomacy, Game.ActionTier.Major), 2 } })); 
    }

    public void FranceBonus()
    {
        Debug.Log("Unflag a Political space in Europe (not Spain or Austria).");
        // Unflag an enemy political space that isn't in Spain or Austria
        List<Space> eligibleSpaces = unflaggableSpaces.Where(space => space.flag == Game.Faction.England).ToList();

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
