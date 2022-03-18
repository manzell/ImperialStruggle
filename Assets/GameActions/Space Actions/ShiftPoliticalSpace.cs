using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class ShiftPoliticalSpace : Action
{
    public enum ActionType { Flag, Deflag }
    public ActionType shiftActionType;
    Space space;

    private void Awake()
    {
        space = GetComponent<Space>();
        SelectInvestmentTilePhase.selectInvestmentTileEvent.AddListener((faction, tile) => Can(faction));
        ChargeActionPoints.chargeActionPointsEvent.AddListener(charge => Can(charge.actingFaction));
        TakeDebt.takeDebtEvent.AddListener(td => Can(td.actingFaction));
    }

    void SetCost(Game.Faction faction)
    { 
        requireMajorAction = space.conflictMarker == false && space.flag != Game.Faction.Neutral;
        actionCost = space.conflictMarker ? 1 : space.flagCost;

        shiftActionType = space.flag == Game.Faction.Neutral ? ActionType.Flag : ActionType.Deflag;
        actionName = space.flag == Game.Faction.Neutral ? "Flag" : "Deflag";
    }

    // TODO - Need to be able to create ShiftSpaceCommands 
    public override bool Can(Game.Faction faction)
    {
        available = true; 
        SetCost(faction); 

        Try.Invoke(this); // Try Happens BEFORE calculating if we can afford it. 

        if (actionCost > 0)
        {
            Player player = Player.players[faction];
            // Check that we have the ActionPoints - the player must manually activate their own Debt or Treaty Points first!
            int availableActionPoints =
                (player.majorActionPoints.TryGetValue(requiredActionType, out int points) ? points : 0) +
                (player.majorActionPoints.TryGetValue(Game.ActionType.Debt, out int debt) ? debt : 0) +
                (player.majorActionPoints.TryGetValue(Game.ActionType.Treaty, out int treaty) ? treaty : 0);

            if (requireMajorAction == false)
                availableActionPoints += player.minorActionPoints.ContainsKey(requiredActionType) ? player.minorActionPoints[requiredActionType] : 0;

            if (availableActionPoints < actionCost) available = false;
            if (space.flag == faction) available = false; // Cannot Diplomatic Action on a Space we already control
            if (requireMajorAction == true && player.majorActionPoints.ContainsKey(requiredActionType) == false) available = false; // Require matching Major Action Type
        }

        return available;
    }

    [Button]
    public void Shift(Game.Faction faction)
    {
        ActionRound actionRound = Phase.currentPhase as ActionRound;
        Dictionary<Game.ActionType, int> charge = new Dictionary<Game.ActionType, int> { { requiredActionType, actionCost } }; 

        actionRound.gameActions.Add(new ChargeActionPoints(faction, charge));
        actionRound.gameActions.Add(new ShiftSpace(space, faction)); 
    }
}