using System;
using System.Collections.Generic;
using GameController;
using GamePlay.Enemies;
using GamePlay.Towers;
using Pool;
using Types;
using UI;
using UnityEngine;

namespace GamePlay
{
    public class BuildingManager : MonoBehaviour
    {
        public static Action TowerSelected = delegate { };
        public static Action TowerUnSelected = delegate { };

        private TowerController _selectedTowerPrefab;

        private void OnEnable()
        {
            InputController.Pressed += InputControllerOnPressed;
            BuildButtonController.Pressed += BuildButtonControllerOnPressed;
            TileController.TileSelected += TileControllerOnTileSelected;
        }

        private void OnDisable()
        {
            InputController.Pressed -= InputControllerOnPressed;
            BuildButtonController.Pressed -= BuildButtonControllerOnPressed;
            TileController.TileSelected -= TileControllerOnTileSelected;
        }

        private void TileControllerOnTileSelected(TileController tileController)
        {
            TowerUnSelected.Invoke();
            ObjectPool.Instance.Get<TowerController>(_selectedTowerPrefab, tileController.SpawnTransform.position,
                tileController.SpawnTransform.rotation);
            _selectedTowerPrefab = null;
        }

        private void BuildButtonControllerOnPressed(TowerType obj)
        {
            SelectTower(obj);
        }

        private void InputControllerOnPressed(TowerType obj)
        {
            SelectTower(obj);
        }

        private void SelectTower(TowerType type)
        {
            var towers = AssetManager.Instance.TowerPrefabs;
            _selectedTowerPrefab = null;
            for (int i = 0; i < towers.Count; i++)
            {
                if (towers[i].Type == type)
                {
                    _selectedTowerPrefab = towers[i].Prefab;
                }
            }

            if (!ReferenceEquals(_selectedTowerPrefab, null))
            {
                TowerSelected.Invoke();
            }
            else
            {
                TowerUnSelected.Invoke();
            }
        }
    }
}