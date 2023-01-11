using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class NativeAmericanAlliances_BR_Base : PlayerAction
    {
        [SerializeField] Map northAmerica;
        new public string Name = "Four Mohawk Kings"; 

        protected override async Task Do()
        {
            IEnumerable<PoliticalSpace> eligibleSpaces = Game.Spaces.OfType<PoliticalSpace>().Where(space => space.map == northAmerica && space.Flag != Player.Faction);

            Selection<Space> selection = new(Player, eligibleSpaces);
            await selection.Completion;

            if (selection.Count() > 0)
                Commands.Push(new ShiftSpaceCommand(selection.First() as FlaggableSpace, Player.Faction)); 
        }
    }

    public class NativeAmericanAlliances_BR_Bonus : PlayerAction
    {
        [SerializeField] Map northAmerica; 
        protected override async Task Do()
        {
            Selection<AdvantageTile> selection = new(Player,
                Player.AdvantageTiles.Where(tile => tile.adjacentSpaces.All(space => space.map == northAmerica))); 

            await selection.Completion;

            if (selection.Count() > 0)
                selection.First().Do(Player); 
        }
    }

    public class NativeAmericanAlliances_FR_Bonus : PlayerAction
    {
        [SerializeField] Map northAmerica;

        protected override async Task Do()
        {
            IEnumerable<PoliticalSpace> eligibleSpaces = Game.Spaces.OfType<PoliticalSpace>().Where(space => space.map == northAmerica && space.Flag != Player.Faction);

            Selection<PoliticalSpace> selection = new(Player, eligibleSpaces);
            await selection.Completion;

            if (selection.Count() > 0)
                Commands.Push(new UnflagCommand(selection.First())); 
        }
    }

}
