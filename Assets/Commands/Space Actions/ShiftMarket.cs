using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class ShiftMarket : Action
{
    public enum ActionType { None, Flag, Deflag }
    public ActionType shiftActionType, requiredShiftType;
    public Game.Faction requiredFaction;
    public int fixedActionCost = -1;
    public int finalCost; 
    Market space;

    private void Awake()
    {
        space = GetComponent<Market>();
        SelectInvestmentTile.selectInvestmentTileEvent.AddListener(tile => Can((Phase.currentPhase as ActionRound).actingFaction));
        AdjustActionPoints.adjustActionPointsEvent.AddListener(charge => Can(charge.actingFaction));
        TakeDebt.takeDebtEvent.AddListener(td => Can(td.actingFaction));
    }

    void SetCost()
    {
        requireMajorAction = space.conflictMarker == false && space.flag != Game.Faction.Neutral;
        finalCost = fixedActionCost >= 0 ? fixedActionCost : Mathf.Clamp(actionCost + (space.conflictMarker ? 1 : space.flagCost), 0, 99);
    }

    void SetActionName(Game.Faction faction)
    {
        Game.Faction opposingFaction = faction == Game.Faction.England ? Game.Faction.France : Game.Faction.England;

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

    // TODO - Need to be able to create ShiftSpaceCommands 
    public override bool Can(Game.Faction faction)
    {
        Game.Faction opposingFaction = faction == Game.Faction.England ? Game.Faction.France : Game.Faction.England;

        available = true;
        SetCost();
        SetActionName(faction);

        Try.Invoke(this); // Try Happens BEFORE calculating if we can afford it. 

        if (finalCost > 0 && (requiredShiftType == ActionType.None || requiredShiftType == shiftActionType))
        {
            Player player = Player.players[faction];
            // Check that we have the ActionPoints - the player must manually activate their own Debt or Treaty Points first!
            int availableActionPoints =
                (player.actionPoints.TryGetValue((requiredActionType, Game.ActionTier.Major), out int points) ? points : 0) +
                (player.actionPoints.TryGetValue((Game.ActionType.Debt, Game.ActionTier.Major), out int debt) ? debt : 0) +
                (player.actionPoints.TryGetValue((Game.ActionType.Treaty, Game.ActionTier.Major), out int treaty) ? treaty : 0);

            if (requireMajorAction == false)
                availableActionPoints += player.actionPoints.ContainsKey((requiredActionType, Game.ActionTier.Minor)) ? player.actionPoints[(requiredActionType, Game.ActionTier.Minor)] : 0;

            if (availableActionPoints < finalCost) available = false;
            if (requireMajorAction == true && player.actionPoints.ContainsKey((requiredActionType, Game.ActionTier.Major)) == false) available = false; // Require matching Major Action Type
        }

        if (requiredFaction != Game.Faction.Neutral && requiredFaction != faction) available = false;
        if (space.flag == faction) available = false; // Cannot Shift A Market we already control

        return available;
    }

    [Button]
    public override void Do(Game.Faction faction)
    {
        SetCost();

        ActionRound actionRound = Phase.currentPhase as ActionRound;
        Dictionary<(Game.ActionType, Game.ActionTier), int> charge = new Dictionary<(Game.ActionType, Game.ActionTier), int> { 
            { (requiredActionType, requireMajorAction ? Game.ActionTier.Major: Game.ActionTier.Minor), -finalCost } };

        actionRound.gameActions.Add(new AdjustActionPoints(faction, charge));
        actionRound.gameActions.Add(new ShiftSpace(space, faction));

        DoEvent.Invoke(this);
    }
}