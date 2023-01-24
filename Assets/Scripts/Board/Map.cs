using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class Map : ScriptableObject, ISelectable
    {
        public string Name => name;
        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }
        public AwardTile awardTile;
        public IEnumerable<Space> spaces => Game.Spaces.Where(space => space.map == this);

        public Dictionary<Faction, int> mapScore => spaces.GroupBy(space => space.Flag).ToDictionary(group => group.Key, group => group.Count());

        public Faction controllingFaction
        {
            get
            {
                int maxGameScore = mapScore.Max(kvp => kvp.Value);
                int winningMargin = maxGameScore - mapScore.Values.OrderByDescending(val => val).ElementAt(1);

                List<Faction> winningFactions = new List<Faction>();

                foreach (Player player in Player.Players)
                    if (mapScore[player.Faction] == maxGameScore && winningMargin >= awardTile.Margin) // Need to move the margin logic out to the ScoreMapAction?
                        winningFactions.Add(player.Faction);

                if (winningFactions.Count == 1)
                    return winningFactions[0];
                else
                    return null;
            }
        }
    }
}