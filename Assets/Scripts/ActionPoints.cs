using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.Utilities;

namespace ImperialStruggle
{
    // Action points is a container class for each player's APs as well as any *activated* deby and treaty points as well as any Victory Points to award
    public class ActionPoints : IEnumerable<ActionPoint>
    {
        public IEnumerator<ActionPoint> GetEnumerator() => actionPoints.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public System.Action AdjustAPEvent;

        List<ActionPoint> actionPoints;

        public ActionPoints() => actionPoints = new();
        public ActionPoints(List<ActionPoint> points) => this.actionPoints = points;

        public bool Can(PurchaseAction context)
        {
            bool retVal = actionPoints.Where(ap => (ap.type == context.ActionCost.type || ap.type >= ActionPoint.ActionType.Free) && ap.tier >= context.ActionCost.tier)
                .Sum(ap => ap.Value(context)) > context.ActionCost.Value(context);

            //Debug.Log($"Testing if {context.Player} can afford {context.Name} ({context.ActionCost.name}) {retVal}");
            return retVal;
        }

        public void Credit(ActionPoints aps) => aps.ForEach(ap => Credit(ap));
        public void Credit(ActionPoint ap)
        {
            if (actionPoints.Contains(ap))
                ap.AdjustBaseValue(ap.baseValue); 
            else
                actionPoints.Add(new(ap));

            AdjustAPEvent?.Invoke();
        }

        public void Charge(PurchaseAction context)
        {
            if(Can(context))
            {
                List<ActionPoint> eligiblePaymentAPs = new();
                ActionPoint actionCost = new(context.ActionCost); 

                foreach(ActionPoint AP in actionPoints)
                {
                    bool typeOK = AP.type == actionCost.type || AP.type > ActionPoint.ActionType.VictoryPoint;
                    bool majorMinorOK = AP.tier == ActionPoint.ActionTier.Major || actionCost.tier == ActionPoint.ActionTier.Minor;
                    bool allConditions = AP.conditionals.All(condition => condition.Test(context));

                    if (typeOK && majorMinorOK && allConditions)
                        eligiblePaymentAPs.Add(AP); 
                }

                while (actionCost.Value(context) > 0 && eligiblePaymentAPs.Any(ap => ap.Value(context) > 0))
                {
                    ActionPoint thisAP = eligiblePaymentAPs.Where(ap => ap.Value(context) > 0)
                        .OrderBy(ap => ap.tier == ActionPoint.ActionTier.Major) // Spend Minor APs before Major APs
                        .ThenByDescending(ap => ap.type == actionCost.type) // Use restricted-type APs before wild-card type APs
                        .ThenByDescending(ap => ap.conditionals.Count()) // default to more-conditions first
                        .FirstOrDefault();

                    if(thisAP != null)
                    {
                        thisAP.AdjustBaseValue(-1);
                        actionCost.AdjustBaseValue(-1);
                    }
                }

                AdjustAPEvent?.Invoke();
            }
        }

        //public int CompareTo(InvestmentTile tile)
        //{
        /*
        if (majorActionType > tile.data.majorActionType) return -1;
        else if (majorActionType == tile.data.majorActionType)
        {
            if (majorActionPoints > tile.data.majorActionPoints) return -1;
            else if (majorActionPoints == tile.data.majorActionPoints)
            {
                if (minorActionType > tile.minorActionType) return -1;
                else if (minorActionType == tile.minorActionType)
                {
                    if (minorActionPoints > tile.minorActionPoints) return -1;
                    else if (minorActionPoints == tile.minorActionPoints) return 0;
                }
            }
        }
        return 1;
        */
        //}
    }

    [System.Serializable]
    public class ActionPoint : ISelectable
    {
        public enum ActionType { None, Finance, Diplomacy, Military, VictoryPoint, Free, Debt, Treaty }
        public enum ActionTier { Minor, Major }

        public override int GetHashCode() => System.HashCode.Combine(type, tier, baseValue, conditionals);
        public override bool Equals(object obj) => GetHashCode() == obj.GetHashCode();

        [field: SerializeField] public ActionTier tier { get; private set; }
        [field: SerializeField] public ActionType type { get; private set; }
        [field: SerializeField] public int baseValue { get; private set; }
        [field: SerializeReference] public List<Conditional> conditionals { get; private set; }

        public string conditionText => conditionals != null && conditionals.Count > 0 ? string.Join(", ", conditionals.Select(c => c.ToString())) : string.Empty;
        public string Name => $"{Value(null)} [{baseValue}] {tier} {type} ({conditionText})";

        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }

        public ActionPoint(ActionTier tier, ActionType type, int baseValue)
        {
            this.tier = tier;
            this.type = type;
            this.baseValue = baseValue;
            conditionals = new(); 
        }

        public ActionPoint(ActionPoint ap)
        {
            tier = ap.tier;
            type = ap.type;
            baseValue = ap.baseValue;

            conditionals = ap.conditionals == null ? new() : ap.conditionals; 
        }

        public static bool operator >(ActionPoint ap1, ActionPoint ap2) =>
            ap1.tier > ap2.tier || (ap1.tier == ap2.tier && ap1.type > ap2.type) ||
            (ap1.tier == ap2.tier && ap1.type == ap2.type && ap1.baseValue > ap2.baseValue) ||
            (ap1.tier == ap2.tier && ap1.type == ap2.type && ap1.baseValue == ap2.baseValue && ap1.conditionals.Count < ap2.conditionals.Count);

        public static bool operator <(ActionPoint ap1, ActionPoint ap2) =>
            ap1.tier < ap2.tier || (ap1.tier == ap2.tier && ap1.type < ap2.type) ||
            (ap1.tier == ap2.tier && ap1.type == ap2.type && ap1.baseValue < ap2.baseValue) ||
            (ap1.tier == ap2.tier && ap1.type == ap2.type && ap1.baseValue == ap2.baseValue && ap1.conditionals.Count > ap2.conditionals.Count);

        public void AdjustBaseValue(int x) => baseValue += x;

        public int Value(PurchaseAction context)
        {
            if (context == null)
            {
                //Debug.Log($"{baseValue} {actionTier} {actionType} | {conditionals}"); 
                if (conditionals == null || conditionals.Count == 0)
                    return baseValue;
                else
                    return 0;
            }
            else if (conditionals.All(condition => condition.Test(context)))
                return baseValue;
            else
                return 0;
        }
    }
}