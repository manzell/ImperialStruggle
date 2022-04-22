using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivateTreatyPoint : Command
{
    public static UnityEvent<ActivateTreatyPoint> activateTreatyPointsEvent = new UnityEvent<ActivateTreatyPoint>();

    public override void Do(Action action)
    {
    }
}
