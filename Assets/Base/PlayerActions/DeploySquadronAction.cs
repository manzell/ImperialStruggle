using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeploySquadronAction : PlayerAction
{
    ActionPoint actionPoint;

    public void Awake()
    {
        actionPoint = new ActionPoint(ActionPoint.ActionType.Military, ActionPoint.ActionTier.Minor);
        actionPointCost.Add(actionPoint);
    }

    public int CalculateDeploymentCost(Player player)
    {
        if (GetComponent<NavalSpace>().flag == player.faction)
            return 1;
        else
        {
            foreach (Squadron squadon in player.squadrons)
                if (squadon.space != null) return 2;

            return 3; 
        }
    }

    public override bool Can()
    {
        actionPoint.actionPoints = CalculateDeploymentCost(player); 

        return base.Can();
    }


}
