using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class TargetSpaceCondition : Conditional
{
    public List<Space> eligibleSpaces = new List<Space>();

    public override bool Test(BaseAction context)
    {
        if (context is ITargetType<Space> space)
            return eligibleSpaces.Contains(space.target);

        if(context is ITargetType<List<Space>> spaceList)
            return spaceList.target.All(space => eligibleSpaces.Contains(space));

        return false; 
            
    }
}
