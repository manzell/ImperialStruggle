using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

[System.Serializable]
public class ActionPoint
{
    public enum ActionType { None, Finance, Diplomacy, Military, Debt, Treaty, Free, VictoryPoint } // move these to ActionPoint
    public enum ActionTier { Minor, Major }

    public ActionPointKey apType; 
    public ActionType actionType;
    public ActionTier actionTier;
    public int actionPoints;
    public List<Conditional> conditionals = new List<Conditional>();

    public int Value(PlayerAction context) => conditionals.All(condition => condition.Test(context)) ? actionPoints : 0;

    public ActionPointKey apKey => new ActionPointKey(this); 

    public ActionPoint(ActionType type, ActionTier tier)
    {
        actionType = type;
        actionTier = tier;
        actionPoints = 1;
    }

    public ActionPoint() { }

    [System.Serializable]
    public struct ActionPointKey
    {
        public ActionType actionType;
        public ActionTier actionTier;
        public string condition; 

        public ActionPointKey(ActionType type, ActionTier tier)
        {
            actionType = type;
            actionTier = tier;
            condition = string.Empty; 
        }

        public ActionPointKey(ActionPoint AP)
        {
            actionType = AP.actionType;
            actionTier = AP.actionTier;
            condition = string.Empty;

            foreach(Conditional c in AP.conditionals)
            {
                if (condition != string.Empty) 
                    condition += " "; 
                condition += $"{c.conditionalText}.";
            }

        }
    }
}
