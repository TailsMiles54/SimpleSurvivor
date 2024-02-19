using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;

    public void SetTarget(Player target)
    {
        _target = target;
        _animator.Play("IdleNormal");
    }

    public void FixedUpdate()
    {
        if(_target != null && _target.transform != null)
            _navMeshAgent.SetDestination(_target.transform.position);
        else
            _target = PhotonNetwork.FindGameObjectsWithComponent(typeof(Player)).Select(x => x.GetComponent<Player>()).First();
    }
}
