using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class SpawnEnemyWaves : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int xMaxPos;
    [SerializeField] private int zMaxPos;
    [SerializeField] private int howManyEnemies;

    private void OnEnable()
    {
        UIBehaviour.onTimeChange += SpawnEnemies;
    }

    private void SpawnEnemies(string state)
    {
        switch (state)
        {
            case "start":
                EnemyWave(howManyEnemies, SetPositions());
                break;
            case "middle":
                EnemyWave(howManyEnemies * 2, SetPositions());
                break;
            case "end":
                EnemyWave(howManyEnemies * 3, SetPositions());
                break;
        }
    }

    private void EnemyWave(int count, Vector3 position)
    {
       for (int i = 0; i < count; i++)
       {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.SetParent(transform);
            enemy.transform.localPosition = position;
       }
    }

    private Vector3 SetPositions()
    {
        var xVal = UnityEngine.Random.Range(-xMaxPos, xMaxPos);
        var zVal = UnityEngine.Random.Range(-zMaxPos, zMaxPos);

        return new Vector3(xVal, 1.35f, zVal);
    }

    private void OnDisable()
    {
        UIBehaviour.onTimeChange -= SpawnEnemies;
    }
}
