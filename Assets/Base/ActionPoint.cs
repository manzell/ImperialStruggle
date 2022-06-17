using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
using System;

[System.Serializable]
public class ActionPoint
{
    public enum ActionType { None, Finance, Diplomacy, Military, Debt, Treaty, Free, VictoryPoint } // move these to ActionPoint
    public enum ActionTier { Minor, Major }

    public ActionType actionType;
    public ActionTier actionTier;
    public int baseValue = 0;
    public string name => $"{baseValue} ({Value(null)}) {actionTier} {actionType}";
    public List<Conditional> conditionals = new List<Conditional>();

    public string conditionText
    {
        get
        {
            string retVal = string.Empty;

            foreach (Conditional c in conditionals)
            {
                if (retVal != string.Empty)
                    retVal += " ";
                retVal += $"{c.conditionalText}";
            }

            return retVal;
        }
    }

    public int Value(PlayerAction context) 
    {
        if (context == null)
        {
            //Debug.Log($"{baseValue} {actionTier} {actionType} | {conditionals}"); 
            if (conditionals.Count == 0) 
                return baseValue;
            else 
                return 0;
        }
        else if (conditionals.All(condition => condition.Test(context)))
            return baseValue;
        else
            return 0;
    }

    public override int GetHashCode() => HashCode.Combine(actionType, actionTier, conditionals);
    public override bool Equals(object obj) => GetHashCode() == obj.GetHashCode();

    public static bool operator >=(ActionPoint ap1, ActionPoint ap2) =>
    ap1.actionType == ActionType.Debt || ap1.actionType == ActionType.Treaty ||
    (ap1.actionType == ap2.actionType && (ap1.actionTier == ActionTier.Major || ap1.actionTier == ap2.actionTier) && ap1.conditionText == ap2.conditionText);

    public static bool operator <=(ActionPoint ap1, ActionPoint ap2) =>
        ap2.actionType == ActionType.Debt || ap2.actionType == ActionType.Treaty ||
        (ap1.actionType == ap2.actionType && (ap1.actionTier == ActionTier.Minor || ap1.actionTier == ap2.actionTier) && ap1.conditionText == ap2.conditionText);

    public ActionPoint(ActionType type, ActionTier tier)
    {
        actionType = type;
        actionTier = tier;
        baseValue = 1;
        conditionals = new List<Conditional>(); 
    }

    public ActionPoint(ActionPoint ap) 
    {
        baseValue = ap.baseValue;
        actionTier = ap.actionTier;
        actionType = ap.actionType;
        conditionals = ap.conditionals;
    }
}
