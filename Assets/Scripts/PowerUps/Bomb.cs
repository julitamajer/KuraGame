using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _delay = 2f;
    [SerializeField] private float _radius = 5f;
    [SerializeField] private AudioSource _boom;

    private float _countdown;
    private bool _hasExploded;

    [SerializeField] private GameObject _explosionEffect; 

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
        GameObject boom = Instantiate(_explosionEffect, transform.position, transform.rotation);
        boom.transform.SetParent(gameObject.transform);
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
        _boom.Play();

        foreach (Collider collider in colliders)
        {
            EnemyHP enemyHP = collider.GetComponent<EnemyHP>();

            if (enemyHP != null)
            {
                enemyHP.TakeDamage(3);
            }
            StartCoroutine(DestroyAfterDelay(1)); 
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}
