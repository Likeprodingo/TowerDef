using System;
using GamePlay.Enemies;
using UnityEngine;

namespace GamePlay
{
    public class TownHallController : MonoBehaviour
    {
        public static event Action TownHallDown = delegate {  };

        [SerializeField] private int _hp = 10;

        private int _currentHealth = 0;
        
        private void OnEnable()
        {
            _currentHealth = _hp;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyController enemyController))
            {
                Process(enemyController.Damage);
                enemyController.Attack();
            }
        }

        public void Process(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                TownHallDown.Invoke();
            }
        }
    }
}