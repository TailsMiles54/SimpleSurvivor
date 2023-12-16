using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Settings;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnSystem : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _testEnemyPrefab;

    private SpawnSettings SpawnSettings => SettingsProvider.Get<SpawnSettings>();
    
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            yield return new WaitForSeconds(10f); //ожидание начала

            for (int j = 0; j < 10; j++) //волны
            {
                var players = PhotonNetwork.FindGameObjectsWithComponent(typeof(Player))
                    .Select(x => x.GetComponent<Player>()).ToList();
                foreach (var player in players)
                {
                    for (int i = 0; i < 20; i++) //кол-во противников
                    {
                        var spawnPosition = GetRandomSpawnPosition(player, players);
                        
                        if(spawnPosition != new Vector3())
                        {
                            var enemy = PhotonNetwork.Instantiate(_testEnemyPrefab.name, spawnPosition, Quaternion.identity);
                            var navMeshAgent = enemy.GetComponent<NavMeshAgent>();
                            navMeshAgent.SetDestination(player.transform.position);
                            yield return new WaitForSeconds(1f);
                        }
                    }

                }
            }
        }
    }
    
    private Vector3 GetRandomSpawnPosition(Player player, List<Player> players)
    {
        Vector3 vec = Random.insideUnitCircle.normalized * SpawnSettings.SpawnRadius;
        
        var pos = new Vector3(vec.x, vec.z, vec.y);

        if (players.Any(x => Vector3.Distance(pos, x.transform.position) < SpawnSettings.MinRadius))
        {
            return new Vector3();
        }
        
        return player.transform.position + pos;
    }
}
