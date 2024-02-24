using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : PickableItems
{
    public delegate void OnPickedUpSeed();
    public static event OnPickedUpSeed onPickedUpSeed;
    public override void OnPickedUp(Collider other)
    {
        onPickedUpSeed?.Invoke();
        Destroy(gameObject);
    }
}
