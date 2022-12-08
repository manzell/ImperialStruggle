using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeploySquadronAction : PlayerAction
{
    ActionPoint actionPoint;

    public void Awake()
    {
        actionPoint = new ActionPoint(ActionPoint.ActionType.Military, ActionPoint.ActionTier.Minor);
    }

    public int CalculateDeploymentCost(Player player)
    {
        if (GetComponent<NavalSpace>().Flag == player.faction)
            return 1;
        else
        {
            foreach (Squadron squadon in player.squadrons)
                if (squadon.space != null) return 2;

            return 3; 
        }
    }

    protected override void Do()
    {
        throw new System.NotImplementedException();
    }
}
