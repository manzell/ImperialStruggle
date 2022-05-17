using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Sirenix.OdinInspector; 

public class ShiftSpaceAction : PlayerAction, ITargetType<Game.Faction>, ITargetType<Space>
{
    public ActionPoints actionPointBonusCost; // This is essentially a Modifier cost, since our actual cost and cost type is determined by the space. TODO: Improve this system
    
    public Game.Faction target => targetFaction;
    Space ITargetType<Space>.target => targetSpace;

    Game.Faction targetFaction;
    Space targetSpace;

    protected override void Do(UnityAction callback)
    {
        targetFaction = player.faction;
        targetSpace = GetComponent<Space>();

        base.Do(callback); 
    }

    [Button]
    public override bool Can()
    {
        // Is the space not already friendly-flagged? 
        targetSpace = GetComponent<Space>();

        ActionPoint.ActionType requiredActionType;
        if (targetSpace is Fort || targetSpace is NavalSpace)
            requiredActionType = ActionPoint.ActionType.Military;
        else if (targetSpace is Market)
            requiredActionType = ActionPoint.ActionType.Finance;
        else if (targetSpace is PoliticalSpace)
            requiredActionType = ActionPoint.ActionType.Diplomacy;
        else
            requiredActionType = ActionPoint.ActionType.None; // This should never occur

        if(player != null)
        {
            // Let's establish the cost of the Action. 
            ActionPoint ap = new ActionPoint();
            ap.actionTier = targetSpace.flag == Game.Faction.Neutral ? ActionPoint.ActionTier.Minor : ActionPoint.ActionTier.Major;
            ap.actionType = requiredActionType;

            ap.actionPoints = targetSpace.flagCost;
            if (targetSpace.conflictMarker)
                ap.actionPoints = 1; 
            if(targetSpace is Market market)
            {
                if (market.isolatedMarket)
                    ap.actionPoints = 1;
                if (market.protectedMarket)
                    ap.actionPoints++;
            }
            if(targetSpace is Fort fort)
            {
                if (fort.damaged)
                    ap.actionPoints = 1; 
            }

            actionPointCost.Clear(); // Each time we call Can() we reset the cost. Think of a better way to handle this?
            actionPointCost.Add(ap);

            return base.Can();
        }
        return false; 
    }
}
