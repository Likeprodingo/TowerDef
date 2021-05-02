using System;
using System.Collections;
using System.Collections.Generic;
using GameController;
using GamePlay.Enemies;
using Pool;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace GamePlay
{
    public class EnemySpawnController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _spawnParticle = default;
        [SerializeField] private TownHallController _townHallController = default;
        [SerializeField] private float _spawnDelay = 0.5f;

        private List<EnemyController> _activeEnemies = new List<EnemyController>();

        private int _enemyCount;

        private void OnEnable()
        {
            EnemyController.EnemyDied += EnemyControllerOnEnemyDied;
        }
        
        private void OnDisable()
        {
            EnemyController.EnemyDied -= EnemyControllerOnEnemyDied;
            foreach (var enemy in _activeEnemies)
            {
                ObjectPool.Instance.FreeObject(enemy);
            }
            _activeEnemies.Clear();
        }

        private void EnemyControllerOnEnemyDied(EnemyController obj)
        {
            _activeEnemies.Remove(obj);
        }

      

        public void SpawnWave(int enemyCount)
        {
            _enemyCount = enemyCount;
            StartCoroutine(SpawnAction());
        }

        private IEnumerator SpawnAction()
        {
            var waiter = new WaitForSeconds(_spawnDelay);
            
            while (_enemyCount > 0)
            {
                _spawnParticle.Play();
                int random = Random.Range(0, AssetManager.Instance.EnemyPrefabs.Count);
                EnemyController prefab = AssetManager.Instance.EnemyPrefabs[random].Prefab;
                EnemyController enemyController =
                    ObjectPool.Instance.Get<EnemyController>(prefab, transform.position, transform.rotation, transform);
                _activeEnemies.Add(enemyController);
                enemyController.SetAim(_townHallController.transform);
                yield return waiter;
                _spawnParticle.Stop();
            }
        }
    }
}