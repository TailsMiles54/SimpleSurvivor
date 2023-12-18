using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
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

            var waves = SpawnSettings.WaveSpawnSettingsList;

            var enemiesSettings = SettingsProvider.Get<EnemiesSetting>();
            
            foreach (var wave in waves)
            {
                var players = PhotonNetwork.FindGameObjectsWithComponent(typeof(Player)).Select(x => x.GetComponent<Player>()).ToList();
                foreach (var player in players)
                {
                    foreach (var enemySpawnSetting in wave.EnemiesSpawnSettings)
                    {
                        for (int i = 0; i != enemySpawnSetting.EnemyCount; i++)
                        {
                            var spawnPosition = GetRandomSpawnPosition(player, players);
                        
                            if(spawnPosition != new Vector3())
                            {
                                var enemySetting = enemiesSettings.GetEnemyByType(enemySpawnSetting.EnemyTypes);
                                
                                var enemy = PhotonNetwork.Instantiate(enemySetting.EnemyPrefab.name, spawnPosition, Quaternion.identity);
                                var navMeshAgent = enemy.GetComponent<TestEnemyController>();
                                var baseEnemy = enemy.GetComponent<BaseEnemy>();
                                baseEnemy.EnemyData = new EnemyData(enemySpawnSetting.EnemyTypes, 2, 5, 100);
                                navMeshAgent.SetTarget(player);
                                yield return new WaitForSeconds(wave.EnemySpawnDelay);
                            }
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
