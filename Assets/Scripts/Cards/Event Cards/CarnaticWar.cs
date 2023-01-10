using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class CarnaticWar_Base : PlayerAction
    {
        [SerializeField] Map india; 
        protected override async Task Do()
        {
            IEnumerable<PoliticalSpace> localAlliances = Game.Spaces.OfType<PoliticalSpace>().Where(space => space.map == india); 

            for(int i = 0; i < localAlliances.Count(); i++)
            {
                Selection<Space> selection = new(Player, Game.Spaces.Where(space => space.map == india));
                selection.SetTitle($"Place a Conflict Market in India ({i + 1} of {localAlliances.Count()})");

                await selection.Completion;

                if (selection.Count() > 0)
                {
                    ConflictMarker marker = new(selection.First(), 1); 
                    Commands.Push(new PlaceConflictMarkerCommand(marker));
                }
            }
        }
    }

    public class CarnaticWar_Bonus : PlayerAction
    {
        [SerializeField] Map india;
        [SerializeField] Resource cotton; 
        protected override async Task Do()
        {
            List<FlaggableSpace> eligibleSpaces = Game.Spaces.OfType<Fort>().Where(fort => !fort.damaged).ToList<FlaggableSpace>();
            eligibleSpaces.AddRange(Game.Spaces.OfType<Market>().Where(market => market.Resource == cotton && market.Flag != Player.Faction));

            Selection<FlaggableSpace> selection = new(Player, eligibleSpaces);
            await selection.Completion; 

            if(selection.Count() > 0)
            {
                FlaggableSpace space = selection.First();

                if (space is Market)
                    Commands.Push(new ShiftSpaceCommand(space, Player.Faction));
                else if (space is Fort fort)
                    Commands.Push(new DamageFortCommand(fort)); 
            }
        }
    }
}
