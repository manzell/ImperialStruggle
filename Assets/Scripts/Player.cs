using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace ImperialStruggle
{
    public class Player : SerializedMonoBehaviour, ISelectable
    {
        public static List<Player> Players = new();

        [field: SerializeField] public Faction Faction { get; private set; }
        [field: SerializeField] public Player Opponent { get; private set; }
        [field: SerializeField] public List<PlayerAction> Actions { get; private set; }
        public List<MinistryCard> Ministers { get; private set; }
        public List<EventCard> Cards { get; private set; }
        public Queue<WarTile> WarTiles { get; private set; }
        public Queue<WarTile> BonusWarTiles { get; private set; }
        public List<Squadron> Squadrons { get; private set; }
        public ActionPoints ActionPoints { get; private set; }
        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }
        public UI_Player UI { get; private set; }

        public string Name => Faction.name;
        public HashSet<MinistryCard.Keyword> Keywords => new HashSet<MinistryCard.Keyword>(Ministers.SelectMany(minister => minister.data.keywords));

        private void Awake()
        {
            Game.ActivePlayer = this;
            Faction.player = this;
            Players.Add(this);

            RecordsTrack.currentDebt.Add(Faction, 0);
            RecordsTrack.debtLimit.Add(Faction, 0);
            RecordsTrack.treatyPoints.Add(Faction, 0);

            ActionRound.ActionRoundEndEvent += ResetActionPoints;

            Ministers = new(); 
            ActionPoints = new();
            Cards = new(); 
            WarTiles = new(Faction.basicWarTiles.OrderBy(x => Random.value));
            BonusWarTiles = new(Faction.advancedWarTiles.OrderBy(x => Random.value));

            foreach (PlayerAction action in Actions)
                action.Setup(this); 
        }

        public void SetUI(UI_Player ui) => UI = ui; 

        void ResetActionPoints(Phase phase)
        {
            Debug.Log($"Resetting Action Points {phase}");
            ActionPoints = new ();
        }
    }
}