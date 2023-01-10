using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class PlaceConflictMarkerAction : PlayerAction, IPlayerAction
    {
        [SerializeReference] Calculation<IEnumerable<Space>> eligibleSpaceCalculation; 
        [SerializeField] List<SpaceData> eligibleSpaces;
        [SerializeField] int ConflictMarkerRemovalCost = 1; 

        protected override async Task Do()
        {
            Selection<Space> selection = null;

            if (eligibleSpaces.Count > 0)
                selection = new(Player, eligibleSpaces.Select(data => Game.SpaceLookup[data]));
            else if (eligibleSpaceCalculation != null)
                selection = new(Player, eligibleSpaceCalculation.Calculate()); 

            if(selection != null)
            {
                await selection.Completion;

                if (selection.Count() > 0)
                    Commands.Push(new PlaceConflictMarkerCommand(new(selection.First(), ConflictMarkerRemovalCost)));
            }
        }
    }

    public class ConflictMarker 
    {
        public int RemovalCost { get; set; } = 1; 
        public Space Space { get; set; }

        public ConflictMarker(Space space, int cost)
        {
            RemovalCost = cost;
            Space = space; 
        }
    }

    public class PlaceConflictMarkerCommand : Command
    {
        ConflictMarker marker;
        public PlaceConflictMarkerCommand(ConflictMarker marker) => this.marker = marker; 

        public override void Do(GameAction action)
        {
            marker.Space.ConflictMarkers.Add(marker); 
        }
    }
}
