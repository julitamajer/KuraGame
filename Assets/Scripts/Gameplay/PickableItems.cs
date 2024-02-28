using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickableItems : MonoBehaviour
{
    [SerializeField] protected AudioSource _sound;

    [SerializeField] protected MeshRenderer _meshRenderer;


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerGunSelector gunSelector) && other.gameObject.CompareTag("Player"))
        {
            _meshRenderer.enabled = false;
            OnPickedUp(other);
        }
    }

    public abstract void OnPickedUp(Collider other);
}
