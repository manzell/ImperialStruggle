using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class AddCardsToDeckAction : GameAction
    {
        [SerializeField] List<EventCard> cards;

        protected override void Do() => Queue(new AddCardsToDeckCommand(cards));
    }
}