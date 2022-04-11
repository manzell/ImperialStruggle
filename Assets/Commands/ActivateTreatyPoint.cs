using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivateTreatyPoint : Command
{
    public static UnityEvent<ActivateTreatyPoint> activateTreatyPointsEvent = new UnityEvent<ActivateTreatyPoint>();
    int amount;
    RecordsTrack recordsTrack;

    public void Do(ActivateTreatyPoint atp)
    {
    }
}
