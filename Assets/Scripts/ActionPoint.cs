using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    [System.Serializable]
    public class ActionPoint
    {
        public enum ActionType { None, Finance, Diplomacy, Military, Debt, Treaty, Free, VictoryPoint }
        public enum ActionTier { Minor, Major }

        public ActionType actionType;
        public ActionTier actionTier;
        public int baseValue = 0;
        public string name => $"{baseValue} ({Value(null)}) {actionTier} {actionType}";

        [SerializeReference] public List<Conditional> conditionals = new ();

        public string conditionText
        {
            get
            {
                string retVal = string.Empty;

                foreach (Conditional c in conditionals)
                {
                    if (retVal != string.Empty)
                        retVal += " ";
                    retVal += $"{c.ConditionalText}";
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

        public override int GetHashCode() => System.HashCode.Combine(actionType, actionTier, baseValue, conditionals);
        public override bool Equals(object obj) => GetHashCode() == obj.GetHashCode();

        /*
        public static bool operator >=(ActionPoint ap1, ActionPoint ap2) =>
        ap1.actionType == ActionType.Debt || ap1.actionType == ActionType.Treaty ||
        (ap1.actionType == ap2.actionType && (ap1.actionTier == ActionTier.Major || ap1.actionTier == ap2.actionTier) && ap1.conditionText == ap2.conditionText);

        public static bool operator <=(ActionPoint ap1, ActionPoint ap2) =>
            ap2.actionType == ActionType.Debt || ap2.actionType == ActionType.Treaty ||
            (ap1.actionType == ap2.actionType && (ap1.actionTier == ActionTier.Minor || ap1.actionTier == ap2.actionTier) && ap1.conditionText == ap2.conditionText);
        */

        public ActionPoint(ActionType type, ActionTier tier, int val = 1)
        {
            actionType = type;
            actionTier = tier;
            baseValue = val;

            if (conditionals == null)
                conditionals = new List<Conditional>();
        }

        public ActionPoint(ActionPoint ap) : this(ap.actionType, ap.actionTier, ap.baseValue)
        {
            conditionals = ap.conditionals;
        }
    }
}