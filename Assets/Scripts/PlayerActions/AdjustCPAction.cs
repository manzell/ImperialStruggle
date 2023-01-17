using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

namespace ImperialStruggle
{
    public class AdjustCPAction : PlayerAction
    {
        [SerializeField] int cpAdjustAmount;
        [SerializeField] Theater theater;

        protected override async Task Do(IAction context)
        {
            for (int i = 0; i < cpAdjustAmount; i++)
            {
                IEnumerable<Space> spaces = theater.map.spaces.Where(space => space.Flag != Player.Faction &&
                    (space is Market || space is Fort ||
                    (space is NavalSpace && Player.Squadrons.Any(squadron => squadron.space == null || squadron.space.map == theater.map)) ||
                    (space is Territory terr && (theater.availableTerritories.Contains(terr.Data) || 
                        terr.conquestLines.Select(data => Game.SpaceLookup[data]).Any(space => space.Flag == Player.Faction && 
                        (space is Fort || space is NavalSpace || space is Territory terr)))))                
                    );

                Selection<Space> selection = new(Player, spaces);
                await selection.Completion; 

                if(selection.Count() > 0)
                {
                    Space space = selection.First();

                    Commands.Push(new FlagSpaceCommand(space as FlaggableSpace, Player.Faction)); 

                    if(space is NavalSpace naval)
                    {
                        Selection<Squadron> squadSelection = new(Player, Player.Squadrons.Where(squad => squad.space == null || squad.space.map == theater.map));

                        await squadSelection.Completion;

                        if (squadSelection.Count() > 0)
                            Commands.Push(new DeploySquadronCommand(squadSelection.First(), naval)); 
                    }
                }
            }
        }
    }
}
