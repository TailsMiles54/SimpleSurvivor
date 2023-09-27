using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnSystem : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _build;
    [SerializeField] private GameObject _testEnemyPrefab;
    [SerializeField] private Transform _enemiesParent;
    
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < 100; i++)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                var enemy = PhotonNetwork.Instantiate(_testEnemyPrefab.name, GetRandomSpawnPosition(), Quaternion.identity);
                var navMeshAgent = enemy.GetComponent<NavMeshAgent>();
                navMeshAgent.SetDestination(_build.transform.position);
                yield return new WaitForSeconds(1f);
            }
        }
    }
    
    public Vector3 GetRandomSpawnPosition()
    {
        int x = Random.Range(0, 2) > 0 ? Random.Range(15, 17) : Random.Range(33, 35);
        int z = Random.Range(0, 2) > 0 ? Random.Range(18, 20) : Random.Range(29, 33);
        
        Vector3 vec = Random.insideUnitCircle.normalized * 40;

        var test = new Vector3(vec.x, vec.z, vec.y);
        
        return _build.transform.position + test;
    }
}
