using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _player;
    [SerializeField] private LayerMask _whatIsGround, _whatIsPlayer;
    [SerializeField] private PlayerGunSelector _gunSelector;

    [Header("Patroling")]
    [SerializeField] private Vector3 _walkPoint;
    private bool _walkPointSet;
    [SerializeField] private float _walkPointRange;

    [Header("Attacking")]
    [SerializeField] private float _timeBetweenAttacks;
    private bool _allreadyAttacked;

    [Header("States")]
    [SerializeField] private float _sightRange, _attackRange;
    [SerializeField] private bool _playerInSightRange, _playerInAttackRange;

    private void Awake()
    {
        _player = GameObject.Find("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer);

        if (!_playerInSightRange && !_playerInAttackRange) Patrolling();
        if (_playerInSightRange && !_playerInAttackRange) ChasePlayer();
        if (_playerInSightRange && _playerInAttackRange) AttackPlayer();
    }

    private void Patrolling()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet)
            _agent.SetDestination(_walkPoint);

        Vector3 distanceToWalkPoint = transform.position - _walkPoint;

        if(distanceToWalkPoint.magnitude <1f)
            _walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = UnityEngine.Random.Range(-_walkPointRange, _walkPointRange);
        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y,  transform.position.z + randomZ);

        if(Physics.Raycast(_walkPoint, -transform.up, 2f, _whatIsGround))
        {
            _walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        _agent.SetDestination(_player.position);
    }

    private void AttackPlayer()
    {
        _agent.SetDestination(transform.position);

        transform.LookAt(_player);

        if (!_allreadyAttacked)
        {
            _gunSelector.activeGun.Shoot();
            

            _allreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        _allreadyAttacked = false;
    }
}
