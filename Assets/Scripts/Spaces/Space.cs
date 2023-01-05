using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Drawers;
using System.Linq;

namespace ImperialStruggle
{
    [System.Serializable]
    public abstract class Space : ISpace, ISelectable
    {
        public SpaceData data;         
        public SpaceData Data => data;
        public List<IPlayerAction> Actions { get; private set; }
        public bool ConflictMarker { get; private set; }

        public string Name => data.name;
        public HashSet<Space> adjacentSpaces { get; private set; } = new();
        public virtual Faction Flag { get; private set; }
        
        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }
        public System.Action updateSpaceEvent;

        public virtual Faction control => ConflictMarker ? null : Flag;
        public Map map => data.Map;
        public Phase.Era availableEra => data.availableEra;

        public Space(SpaceData data)
        {
            this.data = data;
            Flag = data.startingFlag;
        }

        public void SetConflictMarker(bool status) => ConflictMarker = status;
        public void SetFlag(Faction faction)
        {
            Flag = faction;
            updateSpaceEvent?.Invoke(); 
        }
    }

    public class Fort : Space, FlaggableSpace
    {
        public FlagCostCalculation flagCost { get; set; }
        public bool damaged;

        public Fort(FortData data) : base(data) => flagCost = new DefaultFortFlagCost(); 
    }

    public class Market : Space, FlaggableSpace
    {
        public FlagCostCalculation flagCost { get; set; }
        public Resource Resource;

        public Market(MarketData data) : base(data)
        {
            Resource = data.ResourceType;
            flagCost = new DefaultMarketFlagCost(); 
        }

        public bool Protected => !adjacentSpaces.Where(space => space.Flag == this.Flag).Any(space =>
                space is NavalSpace navapSpace || (space is Fort fort && !fort.damaged));

        public bool Isolated(Player player)
        {
            int counter = 99;
            bool retVal = true;
            Space currentSpace = this;
            HashSet<Space> spacesToCheck = new();
            HashSet<Space> checkedSpaces = new();

            while (currentSpace != null && counter > 0)
            {
                counter--;
                if ((currentSpace is Territory || currentSpace is Fort || currentSpace is NavalSpace) && currentSpace.control == player.Faction)
                {
                    //Debug.Log($"{currentSpace.Name} is Eligible connection [{counter}]"); 
                    retVal = false;
                    break;
                }
                else
                {
                    checkedSpaces.Add(currentSpace);

                    //Debug.Log($"{currentSpace.Name} is not Controlled Territory/Fort/NavalSpace. Adding " +
                    //    $"{string.Join(", ", spacesToAdd.Select(space => space.Name))} [{counter}]");

                    spacesToCheck.UnionWith(currentSpace.adjacentSpaces.Where(space => !checkedSpaces.Contains(space)
                        && space.Flag == Flag && !space.ConflictMarker).Except(spacesToCheck));
                    currentSpace = spacesToCheck.FirstOrDefault();
                    spacesToCheck.Remove(currentSpace);
                }
            }

            Debug.Log($"Checking {Name} for Isolated Status: {retVal}");
            return retVal;
        }
    }

    public class NavalSpace : Space, PrestigeSpace
    {
        public NavalSpace(NavalData data) : base(data) => Prestigious = data.PrestigeSpace;

        public Squadron Squadron;
        public override Faction Flag => Squadron?.flag;
        public bool Prestigious { get; private set; }
    }

    public class PoliticalSpace : Space, FlaggableSpace, PrestigeSpace, AllianceSpace
    {
        public FlagCostCalculation flagCost { get; set; }
        public bool Alliance { get; private set; }
        public bool Prestigious { get; private set; }

        public PoliticalSpace(PoliticalData data) : base(data)
        {
            flagCost = new DefaultPoliticalFlagCost(); 
            Alliance = data.AllianceSpace;
            Prestigious = data.PrestigeSpace;
        }
    }

    public class Territory : Space, PrestigeSpace
    {
        public Territory(TerritoryData data) : base(data)
        {
            conquestLines = data.ConquestLines;
            Prestigious = data.PrestigeSpace;
        }

        public int CPCost { get; set; } = 1; 
        public List<SpaceData> conquestLines { get; private set; }
        public bool Prestigious { get; private set; }
    }

    public interface ISpace : ISelectable
    {
        public Faction Flag { get; }
        public SpaceData Data { get; }
        public bool ConflictMarker { get; }
    }

    public interface FlaggableSpace : ISpace
    {
        public FlagCostCalculation flagCost { get; }
        public void SetFlag(Faction faction); 
    }

    public interface PrestigeSpace : ISpace
    {
        public bool Prestigious { get; }
    }

    public interface AllianceSpace : ISpace
    {
        public bool Alliance { get; }
    }
}