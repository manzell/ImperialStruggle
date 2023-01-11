using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class RevealWarTiles : GameAction
    {
        [SerializeField] Theater theater;

        protected override async Task Do()
        {
            foreach(WarTile tile in theater.warTiles)
            {
                Debug.Log($"{tile.faction} reveals {tile.Name} in {theater.Name}");
                foreach (PlayerAction action in tile.Actions)
                {
                    action.Setup(tile.faction.player);
                    theater.AdjustTheaterScore(tile.faction, tile.value);
                    await action.Execute(); 
                }
            }

            foreach (Calculation<int> calc in theater.scoringBonuses)
                foreach (Player player in Player.Players)
                    theater.AdjustTheaterScore(player.Faction, calc.Calculate(player)); 

            if(theater.GetTheaterScore(Game.France) != theater.GetTheaterScore(Game.Britain))
            {
                Faction winningFaction = theater.GetTheaterScore(Game.France) > theater.GetTheaterScore(Game.Britain) ? Game.France : Game.Britain;
                int winningMargin = Mathf.Abs(theater.GetTheaterScore(Game.France) - theater.GetTheaterScore(Game.Britain));
                int key = theater.Spoils.Where(kvp => kvp.Key <= winningMargin).Max(kvp => kvp.Key);

                List<PlayerAction> spoils = theater.Spoils[key].Item1;
                List<PlayerAction> consolation = theater.Spoils[key].Item2;

                foreach (PlayerAction action in spoils)
                {
                    action.Setup(winningFaction.player);
                    await action.Execute(); 
                }

                foreach(PlayerAction action in consolation)
                {
                    action.Setup(winningFaction.Opposition().player);
                    await action.Execute(); 
                }
            }
        }
    }
}
