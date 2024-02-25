using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : HealhtBehaviour
{
    public delegate void OnEnemyDead();
    public static event OnEnemyDead onEnemyDead;

    public override void Dead()
    {
        Debug.Log("Dead " + name);
        onEnemyDead?.Invoke();
        Destroy(gameObject);
    }
}
