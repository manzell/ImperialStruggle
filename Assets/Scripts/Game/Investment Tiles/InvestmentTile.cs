using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
using ImperialStruggle;

public class InvestmentTile : ISelectable
{
    public enum InvestmentTileStatus { Reserve, Available, Drafted, Exhausted }
    public static System.Action<Player, InvestmentTile> selectInvestmentTileEvent;

    public InvestmentTileData data { get; private set; }
    public InvestmentTileStatus status; 
    public ActionPoints actionPoints;

    public InvestmentTile(InvestmentTileData data)
    {
        this.data = data;

        ActionPoint majorAP = new ActionPoint(data.majorActionPoint.actionType, ActionPoint.ActionTier.Major, data.majorActionPoint.baseValue);  
        ActionPoint minorAP = new ActionPoint(data.minorActionPoint.actionType, ActionPoint.ActionTier.Minor, 2);

        actionPoints = new();
        actionPoints.Add(majorAP);
        actionPoints.Add(minorAP);
    }
}
