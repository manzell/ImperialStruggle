using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class VaticanPolitics_FR_Base : PlayerAction
    {
        [SerializeField] HashSet<PoliticalData> spain_austriaSpaces; 

        protected override async Task Do()
        {
            Selection<FlaggableSpace> selection = new(Player, spain_austriaSpaces.Select(d => Game.SpaceLookup[d] as FlaggableSpace).Where(space => space.Flag != Game.France));

            await selection.Completion;

            if (selection.Count() > 0)
                Commands.Push(new ShiftSpaceCommand(selection.First(), Game.France));
        }
    }

    public class VaticanPolitics_FR_Bonus : PlayerAction
    {
        [SerializeField] HashSet<PoliticalData> spain_austriaSpaces;
        [SerializeField] int vpAward = 2; 

        protected override Task Do()
        {
            if (!spain_austriaSpaces.Select(d => Game.SpaceLookup[d]).Any(space => space.Flag == Game.Britain))
                Commands.Push(new AdjustVPCommand(Game.France, vpAward));

            return Task.CompletedTask; 
        }
    }
}
