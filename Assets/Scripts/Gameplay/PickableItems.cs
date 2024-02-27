using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickableItems : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerGunSelector gunSelector) && other.gameObject.CompareTag("Player"))
        {
            OnPickedUp(other);
        }
    }

    public abstract void OnPickedUp(Collider other);
}
