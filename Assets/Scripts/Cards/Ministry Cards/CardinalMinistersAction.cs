using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using Sirenix.OdinInspector; 

public class CardinalMinistersAction : PlayerAction
{
    [SerializeField] int maxDP = 3;
    [SerializeField] List<Space> bonusSpaces = new List<Space>();

    ActionPoint actionPoint;
    ActionPoints _actionPoints = new ActionPoints();
    public ActionPoints actionPoints => _actionPoints;
    [SerializeField] int bonusDP => Mathf.Clamp(bonusSpaces.Count(space => space.flag == actingPlayer.faction), 0, maxDP);


    protected override void Do()
    {
        actionPoint = new ActionPoint(ActionPoint.ActionType.Diplomacy, ActionPoint.ActionTier.Major);
        actionPoint.baseValue = bonusDP;

        actionPoint.actionTier = Phase.CurrentPhase.GetComponent<ActionRound>()?.investmentTile.majorActionType == ActionPoint.ActionType.Diplomacy ? 
            ActionPoint.ActionTier.Major : ActionPoint.ActionTier.Minor;

        Debug.Log($"Cardinal Ministers Award {actionPoint.baseValue} {actionPoint.actionTier} {actionPoint.actionType} {(actionPoint.baseValue == 1 ? "Bonus DP" : "Bonus DPs")}");

        _actionPoints.Clear();
        _actionPoints.Add(actionPoint);
    }
}
