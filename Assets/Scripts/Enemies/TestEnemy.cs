using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyController : MonoBehaviour
{
    private Player _target;
    private NavMeshAgent _navMeshAgent;

    public void SetTarget(Player target)
    {
        _target = target;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void FixedUpdate()
    {
        _navMeshAgent.SetDestination(_target.transform.position);
    }
}
