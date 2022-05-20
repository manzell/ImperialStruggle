using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq; 

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

    // What i'm trying to create here is an entry for each Tile. So basically each different combination of type/tier/condition
    public Dictionary<ActionPoint.ActionPointKey, List<ActionPoint>> suitcase
    {
        get {
            Dictionary<ActionPoint.ActionPointKey, List<ActionPoint>> retVal = new Dictionary<ActionPoint.ActionPointKey, List<ActionPoint>>();

            foreach(ActionPoint ap in this)
            {
                if (retVal.ContainsKey(ap.apKey))
                    retVal[ap.apKey].Add(ap);
                else
                    retVal.Add(ap.apKey, new List<ActionPoint>() { ap });
            }

            return retVal; 
        }
    }

    public Dictionary<ActionPoint.ActionPointKey, int> Values
    {
        get
        {
            Dictionary<ActionPoint.ActionPointKey, int> retVal = new Dictionary<ActionPoint.ActionPointKey, int>();

            foreach (ActionPoint.ActionPointKey key in suitcase.Keys)
                retVal.Add(key, suitcase[key].Sum(AP => AP.Value(null))); 

            return retVal; 
        }
    }
}
