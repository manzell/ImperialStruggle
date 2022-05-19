using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class DeploySquadronCommand : Command
{
    public static UnityEvent<Squadron, Space> deploySquadronEvent = new UnityEvent<Squadron, Space>(); 
    public override void Do(BaseAction action)
    {
        if(action is ITargetType<NavalSpace> naval && action is ITargetType<Squadron> squadron)
        {
            int countEnemySquadrons = naval.target.squadrons.Where(squad => squad.flag != squadron.target.flag).Count();

            NavyBox.squadrons.Remove(squadron.target);
            naval.target.squadrons.Add(squadron.target);
            squadron.target.space = naval.target;            

            foreach (Squadron fleet in naval.target.squadrons)
            {
                if (fleet.flag != squadron.target.flag)
                {
                    naval.target.squadrons.Remove(fleet);
                    NavyBox.squadrons.Add(fleet);
                    fleet.space = null;
                }
            }

            Debug.Log($"{squadron.target.flag} deploys a Fleet to {squadron.target.space}{(squadron.target.space == null ? " from the Navy Box" : $" from " + squadron.target.space.name)}");
            Debug.Log($"Evicting {countEnemySquadrons} from {squadron.target.space.name}");

            deploySquadronEvent.Invoke(squadron.target, naval.target);
            naval.target.updateSpaceEvent.Invoke(); 
        }
    }
}
