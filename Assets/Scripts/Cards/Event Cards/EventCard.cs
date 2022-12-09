using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq;

public class EventCard : ICard, ISelectable
{
    public EventCardData data { get; private set; }

    public string Name => data.name;
    public EventCard(EventCardData data)
    {
        this.data = data;
    }
}
