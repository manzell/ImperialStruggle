using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq; 

public class ShiftPoliticalSpace : Action
{
    public static UnityEvent<ShiftPoliticalSpace> calculateCostEvent = new UnityEvent<ShiftPoliticalSpace>();
    public enum ActionType { None, Flag, Deflag }
    public ActionType shiftActionType, requiredShiftType;
    public Game.Faction requiredFaction;
    public Dictionary<(Game.ActionType, Game.ActionTier), int> fixedActionCost = 
        new Dictionary<(Game.ActionType, Game.ActionTier), int>() { { (Game.ActionType.Diplomacy, Game.ActionTier.Minor), -1 } };

    Space space;

    private void Awake()
    {
        space = GetComponent<Space>();
        SelectInvestmentTile.selectInvestmentTileEvent.AddListener(tile => Can((Phase.currentPhase as ActionRound).actingFaction));
        AdjustAPCommand.adjustActionPointsEvent.AddListener(adjust => Can(adjust.targetFaction));
        TakeDebt.takeDebtEvent.AddListener(td => Can(td.targetFaction));
    }

    void SetCost()
    {
        requireMajorAction = space.conflictMarker == false && space.flag != Game.Faction.Neutral;

        if(fixedActionCost[(Game.ActionType.Diplomacy, Game.ActionTier.Minor)] >= 0)
            finalActionCost = fixedActionCost; 
        else
            finalActionCost = new Dictionary<(Game.ActionType, Game.ActionTier), int>() { 
                { (requiredActionType, requireMajorAction ? Game.ActionTier.Major : Game.ActionTier.Minor), -(space.conflictMarker ? 1 : space.flagCost) } 
            };

        calculateCostEvent.Invoke(this); 
    }

    void SetActionName(Game.Faction faction)
    {
        Game.Faction opposingFaction = faction == Game.Faction.Britain ? Game.Faction.France : Game.Faction.Britain;

        if (space.flag == opposingFaction || space.flag == Game.Faction.Spain || space.flag == Game.Faction.USA)
        {
            shiftActionType = ActionType.Deflag;
            actionName = "Deflag";
        }
        else if (space.flag == faction)
        {
            // This shouldn't happen
        }
        else //space.flag == Game.Faction.Neutral
        {
            shiftActionType = ActionType.Flag;
            actionName = "Flag";
        }
    }

    public override bool Can(Game.Faction faction)
    {
        available = true; 
        /*SetCost(); 
        SetActionName(faction);

        Try.Invoke(this); // Try Happens BEFORE calculating if we can afford it. 

        if (actionCost > 0 && (requiredShiftType == ActionType.None || requiredShiftType == shiftActionType))
        {
            Player player = Player.players[faction];
            // Check that we have the ActionPoints - the player must manually activate their own Debt or Treaty Points first!
            int availableActionPoints =
                (player.actionPoints.TryGetValue((requiredActionType, Game.ActionTier.Major), out Calculation<int> points) ? points.value : 0) +
                (player.actionPoints.TryGetValue((Game.ActionType.Debt, Game.ActionTier.Major), out Calculation<int> debt) ? debt.value : 0) +
                (player.actionPoints.TryGetValue((Game.ActionType.Treaty, Game.ActionTier.Major), out Calculation<int> treaty) ? treaty.value : 0);

            if (requireMajorAction == false)
                availableActionPoints += player.actionPoints.ContainsKey((requiredActionType, Game.ActionTier.Minor)) ? player.actionPoints[(requiredActionType, Game.ActionTier.Minor)].value : 0;

            if (requiredFaction != Game.Faction.Neutral && requiredFaction != faction) available = false; 
            if (availableActionPoints < actionCost) available = false;
            if (space.flag == faction) available = false; // Cannot Diplomatic Action on a Space we already control
            if (requireMajorAction == true && player.actionPoints.ContainsKey((requiredActionType, Game.ActionTier.Major)) == false) available = false; // Require matching Major Action Type
        }
        */
        return available;
    }

    [Button]
    public override void Do(Game.Faction faction)
    {
        ActionRound actionRound = Phase.currentPhase as ActionRound;

        actionRound.gameActions.Add(new ShiftSpace(space, faction));
        actionRound.gameActions.Add(Phase.currentPhase.gameObject.AddComponent<AdjustAPCommand>());

        DoEvent.Invoke(this); 
    }
}