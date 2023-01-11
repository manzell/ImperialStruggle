using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
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

    public class PlaceConflictMarkerAction : PlayerAction, IPlayerAction
    {
        [SerializeReference] Calculation<HashSet<Space>> eligibleSpaces; 
        [SerializeField] int ConflictMarkerRemovalCost = 1; 

        protected override async Task Do()
        {
            Selection<Space> selection = new(Player, eligibleSpaces.Calculate(Player)); 

            await selection.Completion;

            if (selection.Count() > 0)
                Commands.Push(new PlaceConflictMarkerCommand(new(selection.First(), ConflictMarkerRemovalCost)));
        }
    }

    public class PlaceConflictMarkerCommand : Command
    {
        ConflictMarker marker;

        public PlaceConflictMarkerCommand(ConflictMarker marker) => this.marker = marker; 
        public override void Do(GameAction action) => marker.Space.ConflictMarkers.Add(marker); 
    }
}
