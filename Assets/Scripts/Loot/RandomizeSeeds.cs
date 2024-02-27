using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomizeSeeds : MonoBehaviour
{
    public GameObject objectPrefab; // The object to spawn
    public int maxSpawnAttempts = 10;
    public int spawnNumber;// Maximum number of attempts to find a valid spawn point

    void Start()
    {
        // Call the method to spawn objects
        SpawnObjectsOnNavMesh(spawnNumber); // Adjust number of objects to spawn as needed
    }

    void SpawnObjectsOnNavMesh(int numberOfObjects)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Random position within the nav mesh area
            Vector3 randomPoint = new Vector3(
                Random.Range(-45f, 45f), // Half of the nav mesh width
                0f,
                Random.Range(-45f, 45f)  // Half of the nav mesh length
            );

            // Attempt to find a valid spawn point on the nav mesh
            Vector3 spawnPoint = Vector3.zero; // Initialize spawnPoint
            for (int attempt = 0; attempt < maxSpawnAttempts; attempt++)
            {
                if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
                {
                    spawnPoint = hit.position;
                    break;
                }
                else
                {
                    // If the random point is not on the nav mesh, generate a new one
                    randomPoint = new Vector3(
                        Random.Range(-45f, 45f),
                        0f,
                        Random.Range(-45f, 45f)
                    );
                }
            }

            // Instantiate the object at the valid spawn point
            Instantiate(objectPrefab, spawnPoint, Quaternion.identity);
        }
    }

}
