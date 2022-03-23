using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

public class InvestmentTile : SerializedMonoBehaviour, IExhaustable, System.IComparable<InvestmentTile>
{
    public Dictionary<(Game.ActionType, Game.ActionTier), int> actionPoints; 
    public bool eventCardTrigger, milUpgradeTrigger;
    public bool available = true; 

    public Game.ActionType majorActionType
    {
        get
        {
            Game.ActionType retval = Game.ActionType.None; 

            foreach(KeyValuePair<(Game.ActionType type, Game.ActionTier tier), int> kvp in actionPoints)
            {
                if (kvp.Key.tier == Game.ActionTier.Major)
                    retval = kvp.Key.type; 
            }

            return retval; 
        }
    }

    public Game.ActionType minorActionType
    {
        get
        {
            Game.ActionType retval = Game.ActionType.None;

            foreach (KeyValuePair<(Game.ActionType type, Game.ActionTier tier), int> kvp in actionPoints)
            {
                if (kvp.Key.tier == Game.ActionTier.Minor)
                    retval = kvp.Key.type;
            }

            return retval;
        }
    }

    int majorActionPoints => actionPoints[(majorActionType, Game.ActionTier.Major)];
    int minorActionPoints => actionPoints[(minorActionType, Game.ActionTier.Minor)];

    public int CompareTo(InvestmentTile tile)
    {
        if (majorActionType > tile.majorActionType) return -1;
        else if (majorActionType == tile.majorActionType && majorActionPoints > tile.majorActionPoints) return -1;
        else if (majorActionType == tile.majorActionType && majorActionPoints == tile.majorActionPoints && minorActionType > tile.minorActionType) return -1;
        else if (majorActionType == tile.majorActionType && majorActionPoints == tile.majorActionPoints && minorActionType == tile.minorActionType && minorActionPoints > tile.minorActionPoints) return -1;
        else if (majorActionType == tile.majorActionType && majorActionPoints == tile.majorActionPoints && minorActionType == tile.minorActionType && minorActionPoints == tile.minorActionPoints) return 0;
        else return 1; 
    }

    bool _exhausted = false; 
    public bool exhausted { 
        get => _exhausted; 
        set => _exhausted = value; 
    }
}
