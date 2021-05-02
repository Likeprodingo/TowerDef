using System;
using System.Collections;
using Pool;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemies
{
    public class EnemyController : PooledObject
    {
        public static event Action<EnemyController> EnemyDied = delegate {  };

        [SerializeField] private ParticleSystem _bloodParticle = default;
        [SerializeField] private ParticleSystem _damageParticle = default;
        [SerializeField] private NavMeshAgent _navMeshAgent = default;
        [SerializeField] private int _damage = 2;
        [SerializeField] private int _heath = 100;
        [SerializeField] private float _deathAnimationDuration = 0.3f;

        private int _currentHealth;
        
        public int Damage => _damage;

        public override void SpawnFromPool()
        {
            base.SpawnFromPool();
            _currentHealth = _heath;

        }

        public void DealDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                StartCoroutine(DeathAction());
            }
        }

        public void SetAim(Transform aim)
        {
            _navMeshAgent.SetDestination(aim.position);
        }
        
        public void Attack()
        {
            StartCoroutine(AttackAction());
        }

        private IEnumerator AttackAction()
        {
            _navMeshAgent.isStopped = true; 
            yield return new WaitForSeconds(_deathAnimationDuration);
            //_damageParticle.Play();
            ObjectPool.Instance.FreeObject(this);
        }

        private IEnumerator DeathAction()
        {
            //_bloodParticle.Play();
            _navMeshAgent.isStopped = true;
            EnemyDied.Invoke(this);
            yield return new WaitForSeconds(_deathAnimationDuration);
            ObjectPool.Instance.FreeObject(this);
        }
        
    }
}