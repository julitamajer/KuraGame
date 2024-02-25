using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPowerUp : PickableItems
{
    [SerializeField] PowerUpType powerUpType;
    [SerializeField] private List<GameObject> _powerUps = new List<GameObject>();

    public override void OnPickedUp(Collider other)
    {
        Vector3 spawnPosition = transform.position + new Vector3(1.7f, 1.4f, 0);

        switch (powerUpType)
        {
            case PowerUpType.Speed:
                Instantiate(_powerUps[0], spawnPosition, Quaternion.identity);
                break;
            case PowerUpType.Damage:
                Instantiate(_powerUps[1], spawnPosition, Quaternion.identity);
                break;
            case PowerUpType.Bomb:
                Instantiate(_powerUps[2], spawnPosition, Quaternion.identity);
                break;
            case PowerUpType.Protection:
                Instantiate(_powerUps[3], spawnPosition, Quaternion.identity);
                break;
        }

        Destroy(gameObject);
    }
}
