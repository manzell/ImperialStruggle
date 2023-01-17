using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class FamineInIreland_BR : PlayerAction
    {
        [SerializeField] HashSet<PoliticalData> eligibleSpaces; 

        protected override async Task Do(IAction context)
        {
            Selection<FlaggableSpace> selection = new(Player, eligibleSpaces.Where(space => Game.SpaceLookup[space].Flag == Game.France)
                .Select(space => Game.SpaceLookup[space] as FlaggableSpace));

            if((await selection.Completion).Count() > 0)
                Commands.Push(new UnflagCommand(selection.First())); 
        }
    }

    public class FamineInIreland_FR : PlayerAction
    {
        [SerializeField] HashSet<PoliticalData> irelandSpaces;

        protected override Task Do(IAction context)
        {
            foreach(Space space in irelandSpaces.Select(s => Game.SpaceLookup[s]))
            {
                WarTile bonusWarTile = Player.WarTiles.OrderBy(x => Random.value).First();
                Theater jacobiteRebellion = Game.NextWarTurn.theaters.Where(theater => theater.Name == "Jacobite Rebellion").First();

                Commands.Push(new AddWarTileToTheaterCommand(bonusWarTile, jacobiteRebellion)); 
            }

            return Task.CompletedTask; 
        }
    }
}
