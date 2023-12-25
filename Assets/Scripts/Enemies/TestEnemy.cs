using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : MonoBehaviour
{
    private Player _target;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    public void SetTarget(Player target)
    {
        _target = target;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _animator.Play("IdleNormal");
    }

    public void FixedUpdate()
    {
        _navMeshAgent.SetDestination(_target.transform.position);
    }
}
