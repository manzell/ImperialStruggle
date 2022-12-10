using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
    [System.Serializable]
    public abstract class Space : ISpace
    {
        public System.Action updateSpaceEvent;
        public SpaceData data;

        public HashSet<Space> adjacentSpaces { get; private set; } = new();
        public virtual Faction Flag { get; private set; }
        public bool conflictMarker { get; private set; }

        public string name => data.name;
        public virtual Faction control => conflictMarker ? null : Flag;
        public Map map => data.map;
        public Phase.Era availableEra => data.availableEra;

        public Space(SpaceData data)
        {
            this.data = data;
            Flag = data.startingFlag;
        }

        public void SetConflictMarker(bool status) => conflictMarker = status;
        public void SetFlag(Faction faction) => Flag = faction;
    }

    public class Fort : Space, FlaggableSpace
    {
        public Fort(FortData data) : base(data) => FlagCost = data.FlagCost;

        public bool damaged;
        public int FlagCost { get; private set; }
    }

    public class Market : Space, FlaggableSpace
    {
        public Market(MarketData data) : base(data)
        {
            Resource = data.ResourceType;
            FlagCost = data.FlagCost;
        }

        public Resource Resource;
        public int FlagCost { get; private set; }

        public bool isolated;
        public bool unprotected;
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
        public PoliticalSpace(PoliticalData data) : base(data)
        {
            FlagCost = data.FlagCost;
            Alliance = data.AllianceSpace;
            Prestigious = data.PrestigeSpace;
        }

        public int FlagCost { get; private set; }
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

    public interface ISpace
    {
        public Faction Flag { get; }
    }

    public interface FlaggableSpace : ISpace
    {
        public int FlagCost { get; }
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