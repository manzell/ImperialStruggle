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

        public ConflictMarker(Space space, int cost = 1)
        {
            RemovalCost = cost;
            Space = space;
        }
    }

    public class PlaceConflictMarkerCommand : Command
    {
        ConflictMarker marker;

        public PlaceConflictMarkerCommand(ConflictMarker marker) => this.marker = marker;
        public override void Do(IAction context) => marker.Space.ConflictMarkers.Add(marker);
    }

    public class PlaceConflictMarkerResponse : SelectionReceiver<ISelectable>
    {
        [SerializeField] int removalCost = 1;
        public override void OnSelect(Selection<ISelectable> selection)
        {
            if (selection.FirstOrDefault() is Space space) 
                Commands.Push(new PlaceConflictMarkerCommand(new ConflictMarker(space, removalCost)));
        }
    }
}
