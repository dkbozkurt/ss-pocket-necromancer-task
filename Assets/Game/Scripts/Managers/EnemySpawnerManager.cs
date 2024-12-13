using System;
using System.Collections;
using DG.Tweening;
using Game.Scripts.Behaviours;
using PlayableAdsKit.Scripts.Base;
using PlayableAdsKit.Scripts.PlaygroundConnections;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class EnemySpawnerManager : MonoBehaviour
    {
        [SerializeField] private Vector2 _enemySpawnRadiusRange = new Vector2(8f, 8f);

        private Coroutine _waveSpawnCoroutine;
        
        private void OnEnable()
        {
            GameManager.OnGameStarted += StartSpawn;
            GameManager.OnGameStop += GameStop;
        }

        private void OnDisable()
        {
            GameManager.OnGameStarted -= StartSpawn;
            GameManager.OnGameStop -= GameStop;
        }

        private void StartSpawn()
        {
            SpawnMonsters(3);
            _waveSpawnCoroutine = StartCoroutine(WaveCallCoroutine());
        }
        
        private void SpawnMonsters(int enemyCount)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                SpawnSingleMonster();   
            }
        }

        private void SpawnSingleMonster()
        {
            var spawnedMonster = ObjectPoolManager.Instance.GetPooledObject(ObjectName.Enemy).GetComponent<Enemy>();
            Vector3 randomSpawnPosition = new Vector3(UnityEngine.Random.Range(-_enemySpawnRadiusRange.x, _enemySpawnRadiusRange.x), 0f, UnityEngine.Random.Range(-_enemySpawnRadiusRange.y, _enemySpawnRadiusRange.y));
            spawnedMonster.transform.position = randomSpawnPosition;
        }
        
        private IEnumerator WaveCallCoroutine()
        {
            yield return new WaitForSeconds(3f);
            SpawnMonsters(2);
            
            _waveSpawnCoroutine = StartCoroutine(WaveCallCoroutine());
        }
        
        public void GameStop()
        {
            if(_waveSpawnCoroutine != null) StopCoroutine(_waveSpawnCoroutine);
            GameManager.Instance.CanGetInput = false;
        }
    }
}
