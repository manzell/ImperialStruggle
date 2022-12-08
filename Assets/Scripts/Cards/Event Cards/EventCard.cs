using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq; 

public class EventCard : SerializedMonoBehaviour, ICard, ISelectable
{
    [HideInInspector] public UnityAction callback;

    public ActionPoint.ActionType reqdActionType;
    public Phase.Era era;
    public List<PlayerAction> actions = new List<PlayerAction>();
}
