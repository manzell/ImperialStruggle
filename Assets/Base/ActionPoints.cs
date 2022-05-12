using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

public class ActionPoints : List<ActionPoint>
{
    public ActionPoints Merge(ActionPoints ap)
    {
        ActionPoints actionPoints = new ActionPoints();
        actionPoints.AddRange(this);
        actionPoints.AddRange(ap); 

        return actionPoints;
    }

    public ActionPoints Merge(ActionPoint ap)
    {
        ActionPoints actionPoints = new ActionPoints();
        actionPoints.AddRange(this);
        actionPoints.Add(ap);

        return actionPoints;
    }
}
