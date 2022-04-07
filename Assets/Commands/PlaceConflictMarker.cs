using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceConflictMarker : Command
{
    [SerializeField] List<Space> spaces = new List<Space>();
    List<Space> addedSpaces = new List<Space>();

    public PlaceConflictMarker(List<Space> spaces)
    {
        this.spaces = spaces;
    }

    public override void Do(Game.Faction faction)
    {
        addedSpaces.Clear();
        foreach (Space space in spaces)
        {
            if(space.conflictMarker == false)
            {
                space.conflictMarker = true;
                addedSpaces.Add(space); 
            }
        }
    }

    public override void Undo()
    {
        foreach(Space space in addedSpaces)
        {
            space.conflictMarker = false; 
        }
        addedSpaces.Clear();
    }
}
