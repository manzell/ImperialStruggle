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

        public string Name => data.name;
        public HashSet<Space> adjacentSpaces { get; private set; } = new();
        public virtual Faction Flag { get; private set; }
        public bool conflictMarker { get; private set; }
        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }
        public System.Action updateSpaceEvent;

        public virtual Faction control => conflictMarker ? null : Flag;
        public Map map => data.map;
        public Phase.Era availableEra => data.availableEra;

        public Space(SpaceData data)
        {
            this.data = data;
            Flag = data.startingFlag;
        }

        public void SetConflictMarker(bool status) => conflictMarker = status;
        public void SetFlag(Faction faction)
        {
            Flag = faction;
            updateSpaceEvent?.Invoke(); 
        }
    }

    public class Fort : Space, FlaggableSpace
    {
        public bool damaged;

        public Fort(FortData data) : base(data) { }

        public int GetFlagCost(Player player) => (data as FortData).FlagCost;
    }

    public class Market : Space, FlaggableSpace
    {
        public Resource Resource;
        public Market(MarketData data) : base(data)
        {
            Resource = data.ResourceType;
        }

        public int GetFlagCost(Player player)
        {
            int cost = conflictMarker ? 1 : (data as MarketData).FlagCost;
            return cost; 
        }

        public bool Protected => !adjacentSpaces.Where(space => space.Flag == this.Flag).Any(space =>
                space is NavalSpace navapSpace || (space is Fort fort && !fort.damaged)); 
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
        public virtual int GetFlagCost(Player player) => conflictMarker == true ? 1 : (data as PoliticalData).FlagCost;

        public PoliticalSpace(PoliticalData data) : base(data)
        {
            Alliance = data.AllianceSpace;
            Prestigious = data.PrestigeSpace;
        }

        public bool Alliance { get; private set; }
        public bool Prestigious { get; private set; }
    }

    public class Territory : Space, PrestigeSpace
    {
        public Territory(TerritoryData data) : base(data)
        {
            conquestLines = data.ConquestLines;
            Prestigious = data.PrestigeSpace;
        }

        public List<SpaceData> conquestLines { get; private set; }
        public bool Prestigious { get; private set; }
    }

    public interface ISpace : ISelectable
    {
        public Faction Flag { get; }
    }

    public interface FlaggableSpace : ISpace
    {
        public int GetFlagCost(Player player); 
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