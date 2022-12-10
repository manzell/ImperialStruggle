using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace ImperialStruggle
{
    public class AddCardsToDeckAction : GameAction
    {
        [SerializeField] List<EventCardData> cards;

        protected override void Do() => commands.Add(new AddCardsToDeckCommand(cards));
    }
}