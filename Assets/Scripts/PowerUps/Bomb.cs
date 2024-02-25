using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float _delay = 2f;
    [SerializeField] float _radius = 5f;

    private float _countdown;
    private bool _hasExploded;

    private void Start()
    {
        _countdown = _delay;
    }

    private void Update()
    {
        _countdown -= Time.deltaTime;

        if (_countdown <= 0 && !_hasExploded)
        {
            Explode();
            _hasExploded = true;
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider collider in colliders)
        {
            EnemyHP enemyHP = collider.GetComponent<EnemyHP>();

            if (enemyHP != null)
            {
                enemyHP.TakeDamage(3);
            }
        }

        Destroy(gameObject);
    }
}
