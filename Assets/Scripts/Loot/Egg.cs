using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Egg;

public class Egg : PickableItems
{
    public delegate void OnPickedEgg(Vector3 posion);
    public static event OnPickedEgg onPickedEgg;
    public override void OnPickedUp(Collider other)
    {
        onPickedEgg?.Invoke(transform.position);
        Destroy(gameObject);
    }
}
