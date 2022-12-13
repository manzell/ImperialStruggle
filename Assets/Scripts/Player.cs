using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace ImperialStruggle
{
    public class Player : SerializedMonoBehaviour, ISelectable
    {
        [field: SerializeField] public Faction faction { get; private set; }
        [field: SerializeField] public Player Opponent { get; private set; }
        [field: SerializeField] public Dictionary<MinistryCardData, MinistryCard.MinistryCardStatus> ministers { get; private set; }
        public List<EventCard> hand;
        public Queue<WarTile> warTiles { get; private set; }
        public Queue<WarTile> bonusWarTiles { get; private set; }
        public List<Squadron> squadrons { get; private set; }
        public ActionPoints actionPoints { get; private set; } = new ();
        public UI_Player UI;

        public HashSet<MinistryCard.Keyword> Keywords => new HashSet<MinistryCard.Keyword>(ministers.SelectMany(minister => minister.Key.keywords));

        public static List<Player> players = new();

        public string Name => faction.name;

        private void Awake()
        {
            Game.ActivePlayer = this;
            faction.player = this;
            players.Add(this);

            RecordsTrack.currentDebt.Add(faction, 0);
            RecordsTrack.debtLimit.Add(faction, 0);
            RecordsTrack.treatyPoints.Add(faction, 0);

            ActionRound.PhaseEndEvent += ResetActionPoints;

            warTiles = new(faction.basicWarTiles.OrderBy(x => Random.value));
            bonusWarTiles = new(faction.advancedWarTiles.OrderBy(x => Random.value)); 
            ministers = faction.ministers.ToDictionary(card => card, card => MinistryCard.MinistryCardStatus.Reserved);
        }

        void ResetActionPoints(Phase phase)
        {
            Debug.Log($"Resetting Action Points {phase}");
            actionPoints = new ActionPoints();
        }

        [Button]
        public bool CanAffordAction(PlayerAction action)
        {
            return true;
            /*
            ActionPoints apCosts = new ActionPoints(action.actionPointCost);
            ActionPoints playerAPs = new ActionPoints(actionPoints);

            foreach (ActionPoint apCost in apCosts.Where(ap => ap.baseValue > 0))
            {
                foreach (ActionPoint playerAP in playerAPs.Where(ap => ap.Value(action) > 0))
                {
                    if(playerAP >= apCost) // note this is an actionPoint comparison which returns true if the first arg is eligible to pay for the 2nd
                    {
                        int amtToCharge = Mathf.Min(playerAP.Value(action), apCost.baseValue);

                        apCost.baseValue -= amtToCharge;
                        playerAP.baseValue -= amtToCharge;
                    }
                }

                if (apCost.baseValue > 0)
                    return false; 
            }

            return true;
            */
        }
    }
}