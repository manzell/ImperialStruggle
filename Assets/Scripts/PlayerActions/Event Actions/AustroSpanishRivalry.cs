using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ImperialStruggle
{
    public class AustroSpanish_BR_Base : PlayerAction
    {
        [SerializeField] HashSet<PoliticalData> spanishSpaces; 

        protected override async Task Do()
        {
            Selection<PoliticalSpace> selection = new (Player, spanishSpaces.Select(space => Game.SpaceLookup[space] as PoliticalSpace));

            await selection.Completion;

            if (selection.Count() > 0)
            {
                ConflictMarker marker = new(selection.First(), 1);
                Commands.Push(new PlaceConflictMarkerCommand(marker)); 
            }
        }
    }

    public class AustroSpanish_BR_Bonus : PlayerAction
    {
        protected override async Task Do()
        {
            IEnumerable<Theater> theaters = Game.NextWarTurn.theaters.Where(theater =>
                theater.warTiles.Any(tile => tile.faction == Game.France && tile.warTileSet == WarTile.WarTileSet.Bonus));

            if(theaters.Count() > 0)
                await new Selection<Theater>(Player, theaters, Finish).Completion;
        }

        void Finish(Selection<Theater> selection) 
        {
            WarTile warTile = selection.First().warTiles.Where(tile => tile.faction == Game.France && tile.warTileSet == WarTile.WarTileSet.Bonus).OrderBy(x => Random.value).First();             
            Commands.Push(new RemoveWarTileCommand(warTile));
        }
    }

    public class AustroSpanish_FR_Base : PlayerAction
    {
        [SerializeField] HashSet<PoliticalData> spaces; 

        protected override async Task Do()
        {
            Selection<PoliticalSpace> select = new(Player, 
                spaces.Where(space => Game.SpaceLookup[space].Flag == Game.Britain).Select(space => Game.SpaceLookup[space] as PoliticalSpace));

            await select.Completion;

            Debug.LogWarning("WARNING: Non-callback delayed execution...");

            Commands.Push(new UnflagCommand(select.First())); 
        }

        void Finish(Selection<PoliticalSpace> select) { }
    }

    public class AustroSpanish_FR_Bonus : PlayerAction
    {
        [SerializeField] Map india; 

        protected override async Task Do()
        {
            List<ActionPoint> APs = new() { 
                new(ActionPoint.ActionTier.Major, ActionPoint.ActionType.Diplomacy, 2), 
                new(ActionPoint.ActionTier.Major, ActionPoint.ActionType.Finance, 2) 
            };

            Selection<ActionPoint> selection = new(Player, APs);

            await selection.Completion;

            ActionPoint AP = selection.First();
            AP.conditionals.Add(new TargetMapCondition(india));
            Commands.Push(new AddAPCommand(Player, AP)); 
        }
    }
}
