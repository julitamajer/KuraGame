using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : PickableItems
{
    public delegate void OnPickedUpClock(float addedTime);
    public static event OnPickedUpClock onPickedUpClock;

    public override void OnPickedUp(Collider other)
    {
        onPickedUpClock?.Invoke(5.0f);
        Destroy(gameObject);
    }
}