using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Egg;

public class Egg : PickableItems
{
    public delegate void OnPickedEgg();
    public static event OnPickedEgg onPickedEgg;
    public override void OnPickedUp(Collider other)
    {
        onPickedEgg?.Invoke();
        Destroy(gameObject);
    }
}
