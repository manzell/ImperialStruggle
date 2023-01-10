using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class Squadron : ISelectable
    {
        public Faction flag;
        public NavalSpace space;

        public Action UISelectionEvent { get; set; }
        public Action UIDeselectEvent { get; set; }

        public string Name => space == null ? $"{flag.Name} Squadron (Port)" : $"{flag.Name} in {space.Name}"; 
    }
}