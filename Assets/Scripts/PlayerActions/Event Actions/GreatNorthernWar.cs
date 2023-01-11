using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class GreatNorthernWar_BR : PlayerAction
    {
        [SerializeField] HashSet<PoliticalData> germanStates;
        [SerializeField] int VPaward = 2; 

        protected override async Task Do()
        {
            Selection<FlaggableSpace> selection = new(Player, germanStates.Select(data => Game.SpaceLookup[data] as FlaggableSpace)
                .Where(space => space.Flag != Player.Faction));

            await selection.Completion;

            if (selection.Count() > 0)
                Commands.Push(new ShiftSpaceCommand(selection.First(), Player.Faction));

            Debug.LogWarning("The timing of the shift may not register before we check below. We need to invent a ConditionalCommand that has a conditional " +
                "that gets evaluated only at execution time"); 

            if (germanStates.Select(data => Game.SpaceLookup[data]).All(space => space.Flag == Game.Britain))
                Commands.Push(new AdjustVPCommand(Player.Faction, VPaward)); 
        }
    }

    public class GreatNorthernWar_FR : PlayerAction
    {
        [SerializeField] PoliticalData russia;
        [SerializeField] int VPAward = 2; 

        protected override Task Do()
        {
            if (Game.SpaceLookup[russia].Flag == Game.France)
                Commands.Push(new AdjustVPCommand(Player.Faction, VPAward));
            else
                Commands.Push(new ShiftSpaceCommand(Game.SpaceLookup[russia] as PoliticalSpace, Player.Faction));

            return Task.CompletedTask; 
        }
    }
}
