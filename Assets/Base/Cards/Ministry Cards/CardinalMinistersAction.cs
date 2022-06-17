using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using Sirenix.OdinInspector; 

public class CardinalMinistersAction : PlayerAction, IAdjustAP
{
    [SerializeField] int maxDP = 3;
    [SerializeField] List<Space> bonusSpaces = new List<Space>();

    ActionPoint actionPoint;
    ActionPoints _actionPoints = new ActionPoints();
    public ActionPoints actionPoints => _actionPoints;
    [SerializeField] int bonusDP => Mathf.Clamp(bonusSpaces.Count(space => space.flag == player.faction), 0, maxDP);

    Player IAdjustAP.player => player; 

    public override void Do(UnityAction callback)
    {
        actionPoint = new ActionPoint(ActionPoint.ActionType.Diplomacy, ActionPoint.ActionTier.Major);
        actionPoint.baseValue = bonusDP;

        actionPoint.actionTier = Phase.currentPhase.GetComponent<ActionRound>()?.investmentTile.majorActionType == ActionPoint.ActionType.Diplomacy ? 
            ActionPoint.ActionTier.Major : ActionPoint.ActionTier.Minor;

        Debug.Log($"Cardinal Ministers Award {actionPoint.baseValue} {actionPoint.actionTier} {actionPoint.actionType} {(actionPoint.baseValue == 1 ? "Bonus DP" : "Bonus DPs")}");

        _actionPoints.Clear();
        _actionPoints.Add(actionPoint);

        base.Do(callback);
    }

    public override bool Can()
    {
        if (Phase.currentPhase.TryGetComponent(out ActionRound actionRound))
            return actionRound.investmentTile.actionPoints.Any(ap => ap.actionType == ActionPoint.ActionType.Diplomacy) && bonusDP > 0 && base.Can();
        else
            return false; 
    }

}
