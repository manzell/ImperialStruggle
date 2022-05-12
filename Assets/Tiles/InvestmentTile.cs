using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using Sirenix.OdinInspector;
using System.Linq; 

public class InvestmentTile : SerializedMonoBehaviour, System.IComparable<InvestmentTile>, ISelectable
{
    public enum InvestmentTileStatus { Reserve, Available, Active, Exhausted }

    [HideInInspector] public static UnityEvent<InvestmentTile> selectInvestmentTileEvent = new UnityEvent<InvestmentTile>();
    public InvestmentTileStatus status; 
    public ActionPoints actionPoints;
    List<PlayerAction> selectionActions = new List<PlayerAction>();

    public Game.ActionType majorActionType => actionPoints.Where(ap => ap.actionTier == Game.ActionTier.Major).First().actionType; 
    public Game.ActionType minorActionType => actionPoints.Where(ap => ap.actionTier == Game.ActionTier.Minor).First().actionType;
    public int majorActionPoints => actionPoints.Where(ap => ap.actionTier == Game.ActionTier.Major).Sum(ap => ap.actionPoints);
    public int minorActionPoints => actionPoints.Where(ap => ap.actionTier == Game.ActionTier.Minor).Sum(ap => ap.actionPoints);
    //Note that we don't do any checking of context here - we only test that when trying to use for a player action. 

    public int CompareTo(InvestmentTile tile)
    {
        if (this.majorActionType > tile.majorActionType) return -1;
        else if(this.majorActionType == tile.majorActionType)
        {
            if (this.majorActionPoints > tile.majorActionPoints) return -1; 
            else if(this.majorActionPoints == tile.majorActionPoints)
            {
                if (this.minorActionType > tile.minorActionType) return -1;
                else if(this.minorActionType == tile.minorActionType)
                {
                    if(this.minorActionPoints > tile.minorActionPoints) return -1;
                    else if(this.minorActionPoints == tile.minorActionPoints) return 0;
                }
            }
        }
        return 1; 
    }
}
