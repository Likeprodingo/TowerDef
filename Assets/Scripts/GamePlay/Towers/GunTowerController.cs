using UnityEngine;

namespace GamePlay.Towers
{
    public class GunTowerController : TowerController
    {
        [SerializeField] private Transform _gun = default;
        [SerializeField] private Transform _shootPoint = default;
    }
}