using System;
using System.Collections.Generic;
using GamePlay.Enemies;
using GamePlay.Towers;
using Types;
using UnityEngine;
using Util;

namespace GameController
{
    public class AssetManager : GameObjectSingleton<AssetManager>
    {
        [SerializeField] private List<TowerPrefab> _towerPrefabs = new List<TowerPrefab>();
        [SerializeField] private List<EnemyPrefab> _enemyPrefabs = new List<EnemyPrefab>();

        public List<TowerPrefab> TowerPrefabs => _towerPrefabs;
        public List<EnemyPrefab> EnemyPrefabs => _enemyPrefabs;

        [Serializable]
        public class TowerPrefab
        {
            [SerializeField] private TowerType _type = TowerType.GunTower;
            [SerializeField] private TowerController _prefab;
            [SerializeField] private int _poolCount = 10;

            public TowerType Type => _type;

            public TowerController Prefab => _prefab;

            public int PoolCount => _poolCount;
        }
        
        [Serializable]
        public class EnemyPrefab
        {
            [SerializeField] private EnemyController _prefab;
            [SerializeField] private int _poolCount = 20;

            public EnemyController Prefab => _prefab;

            public int PoolCount => _poolCount;
        }
    }
}