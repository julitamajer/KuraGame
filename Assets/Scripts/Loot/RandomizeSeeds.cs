using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomizeSeeds : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private int _maxSpawnAttempts = 10;
    [SerializeField] private int _spawnNumber;

    void Awake()
    {
        SpawnObjectsOnNavMesh(_spawnNumber);
    }

    void SpawnObjectsOnNavMesh(int numberOfObjects)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(-45f, 45f),
                0f,
                Random.Range(-45f, 45f));

            Vector3 spawnPoint = Vector3.zero;
            for (int attempt = 0; attempt < _maxSpawnAttempts; attempt++)
            {
                if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
                {
                    spawnPoint = hit.position;
                    break;
                }
                else
                {
                    randomPoint = new Vector3(
                        Random.Range(-45f, 45f),
                        0f,
                        Random.Range(-45f, 45f)
                    );
                }
            }

            GameObject seed = Instantiate(_objectPrefab, spawnPoint, Quaternion.identity);
        }
    }
}
