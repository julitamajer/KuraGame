using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : PickableItems
{
    public delegate void OnPickedEgg();
    public static event OnPickedEgg onPickedEgg;
    public override void OnPickedUp(Collider other)
    {
        
        onPickedEgg?.Invoke();
        _sound.Play();
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
