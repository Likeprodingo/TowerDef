using GamePlay;
using GamePlay.Towers;
using Pool;
using UnityEngine;

namespace GameController
{
    public class LevelController : PooledObject
    {
        [SerializeField] private TownHallController _townHallController;
        [SerializeField] private EnemySpawnController _enemySpawnController;
        
        public void StartLevel()
        {
            _enemySpawnController.SpawnWave(30);
        }
    }
}