using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ChicksMoving : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Vector3 _walkPoint;
    [SerializeField] private float _walkPointRange;

    private NavMeshAgent _agent;
    private bool _walkPointSet;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!_walkPointSet)
            SearchWalkPoint();

        if (_walkPointSet)
            _agent.SetDestination(_walkPoint);

        Vector3 distanceToWalkPoint = transform.position - _walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = Random.Range(-_walkPointRange, _walkPointRange);
        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(_walkPoint, -transform.up, 2f, _whatIsGround))
        {
            _walkPointSet = true;
        }
    }
}
