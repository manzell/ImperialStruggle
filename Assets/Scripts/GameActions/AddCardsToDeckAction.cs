using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class AddCardsToDeckAction : GameAction
    {
        [SerializeField] List<EventCard> cards;

        protected override Task Do()
        {
            Queue(new AddCardsToDeckCommand(cards));

            return Task.CompletedTask;
        }
    }
}