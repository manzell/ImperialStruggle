using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class TropicalDiseases_Base : PlayerAction
    {
        [SerializeField] Map carribean; 

        protected override Task Do()
        {
            Selection<Market> selection = new(Player, Game.Spaces.OfType<Market>().Where(market => market.Flag == Player.Opponent.Faction && market.map == carribean));

            if (selection.Count() > 0)
                Commands.Push(new UnflagCommand(selection.First())); 

            selection = new(Player, Game.Spaces.OfType<Market>().Where(market => market != selection.First() && market.Flag == Player.Faction && market.map == carribean));

            if (selection.Count() > 0)
                Commands.Push(new UnflagCommand(selection.First()));

            return Task.CompletedTask; 

        }
    }

    public class TropicalDiseases_Bonus : PlayerAction
    {
        [SerializeField] Map carribean;

        protected override Task Do()
        {
            Selection<Market> selection = new(Player, Game.Spaces.OfType<Market>().Where(market => market.Flag == Player.Opponent.Faction && market.map == carribean));

            if (selection.Count() > 0)
                Commands.Push(new UnflagCommand(selection.First()));

            return Task.CompletedTask; 
        }
    }
}
