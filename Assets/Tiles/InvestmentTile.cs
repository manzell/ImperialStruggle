using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using Sirenix.OdinInspector;
using System.Linq; 

public class InvestmentTile : SerializedMonoBehaviour, System.IComparable<InvestmentTile>
{
    public enum InvestmentTileStatus { Reserve, Available, Active, Exhausted }

    [HideInInspector] public static UnityEvent<InvestmentTile> selectInvestmentTileEvent = new UnityEvent<InvestmentTile>();
    public InvestmentTileStatus status; 
    public ActionPoints actionPoints; 

    public Game.ActionType majorActionType
    {
        get
        {
            Game.ActionType retval = Game.ActionType.None;

            //foreach (AdjustAPCommand command in FindObjectsOfType<AdjustAPCommand>())
            //    foreach(ActionPoint actionPoint in command.actionPoints)
            //        if (actionPoint.actionTier == Game.ActionTier.Major)
            //            retval = actionPoint.actionType; 

            return retval; 
        }
    }

    public Game.ActionType minorActionType
    {
        get
        {
            Game.ActionType retval = Game.ActionType.None;

            //foreach (AdjustAPCommand command in FindObjectsOfType<AdjustAPCommand>())
            //    foreach (ActionPoint actionPoint in command.actionPoints)
            //        if (actionPoint.actionTier == Game.ActionTier.Minor)
            //            retval = actionPoint.actionType;

            return retval; 
        }
    }

    //int majorActionPoints => FindObjectsOfType<AdjustAPCommand>()
    //            .Where(apCommand => apCommand.actionPoints.First().actionTier == Game.ActionTier.Major)
    //            .Sum(apCommand => apCommand.actionPoints.First().Value(new List<ICriteria>()));

    //int minorActionPoints => FindObjectsOfType<AdjustAPCommand>()
    //            .Where(apCommand => apCommand.actionPoints.First().actionTier == Game.ActionTier.Minor)
    //            .Sum(apCommand => apCommand.actionPoints.First().Value(new List<ICriteria>()));

    public int CompareTo(InvestmentTile tile)
    {
        //if (majorActionType > tile.majorActionType) return -1;
        //else if(majorActionType == tile.majorActionType)
        //{
        //    if (majorActionPoints > tile.majorActionPoints) return -1; 
        //    else if(majorActionPoints == tile.majorActionPoints)
        //    {
        //        if (minorActionType > tile.minorActionType) return -1;
        //        else if(minorActionType == tile.minorActionType)
        //        {
        //            if(minorActionPoints > tile.minorActionPoints) return -1;
        //            else if(minorActionPoints == tile.minorActionPoints) return 0;
        //        }
        //    }
        //}
        return 1; 
    }
}
