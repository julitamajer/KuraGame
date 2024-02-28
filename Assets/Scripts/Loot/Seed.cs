using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Seed : PickableItems
{
    public delegate void OnPickedUpSeed();
    public static event OnPickedUpSeed onPickedUpSeed;
    public override void OnPickedUp(Collider other)
    {
        onPickedUpSeed?.Invoke();
        _sound.Play();
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
