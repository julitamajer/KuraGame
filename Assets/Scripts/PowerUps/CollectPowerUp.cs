using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPowerUp : PickableItems
{
    [SerializeField] PowerUpType powerUpType;
    [SerializeField] private List<GameObject> _powerUps = new List<GameObject>();

    public override void OnPickedUp(Collider other)
    {
        switch (powerUpType)
        {
            case PowerUpType.Speed:
                Vector3 spawnPosition = transform.position + new Vector3(1.7f, 1.4f, 0);
                Instantiate(_powerUps[0], spawnPosition, Quaternion.identity);
                break;
        }

        Destroy(gameObject);
    }
}
