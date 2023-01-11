using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
    public class Theater : Phase, ISelectable
    {
        public string Name => name;
        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }
        [field: SerializeField] public Map map { get; private set; }
        [field: SerializeField] public List<WarTile> warTiles { get; private set; } = new();
        [field: SerializeField] public List<Calculation<int>> scoringBonuses { get; private set; }
        [field: SerializeField] public Dictionary<int, (List<PlayerAction>, List<PlayerAction>)> Spoils { get; private set; }
        [field: SerializeField] public List<TerritoryData> availableTerritories { get; private set; }
        [field: SerializeField] public List<PlayerAction> SpecialActions { get; private set; }

        Dictionary<Faction, int> theaterScore;

        public int GetTheaterScore(Faction faction) => theaterScore[faction];
        public void AdjustTheaterScore(Faction faction, int amount) => theaterScore[faction] += amount; 

        void Start()
        {
            theaterScore = new() { { Game.Britain, 0}, { Game.France, 0} };
        }

        /*
        public Faction winningFaction;

        public Dictionary<Faction, int> theaterScore => new Dictionary<Faction, int>() {
        { Game.Britain, scoringBonuses.Where(bonus => bonus.scoringFaction == Game.Britain).Count()
            +  warTiles.Where(tile => tile.faction == Game.Britain).Sum(tile => tile.value) },
        { Game.France, scoringBonuses.Where(bonus => bonus.scoringFaction == Game.France).Count()
            +  warTiles.Where(tile => tile.faction == Game.France).Sum(tile => tile.value) }
        };
        */

        public override bool Completed => throw new System.NotImplementedException();


    }
}