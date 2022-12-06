using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq; 

public class ActionPoints : HashSet<ActionPoint>
{
    public ActionPoints Merge(ActionPoints ap)
    {
        UnionWith(ap);
        return this;
    }

    public ActionPoints Merge(ActionPoint ap)
    {
        Add(ap);
        return this;
    }

    public ActionPoints(ActionPoints points)
    {
        foreach (ActionPoint ap in points)
            Add(new ActionPoint(ap));
    }

    public ActionPoints() { }

    public Dictionary<string, int> Values
    {
        get
        {
            Dictionary<string, int> retVal = new Dictionary<string, int>();
            
            foreach(ActionPoint ap in this)
            {
                if (retVal.ContainsKey(ap.name))
                    retVal[ap.name] += ap.baseValue;
                else
                    retVal.Add(ap.name, ap.baseValue); 
            }

            return retVal; 
        }
    }
}
