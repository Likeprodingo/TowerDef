using System;
using Types;
using UI;
using UnityEngine;

namespace GamePlay.Towers
{
    public class TileController : MonoBehaviour
    {
        public static event Action<TileController> TileSelected = delegate {  };

        [SerializeField] private ParticleSystem _selectedParticleSystem = default;
        [SerializeField] private Transform _spawnTransform = default;
        [SerializeField] private Material _highlightMaterial = default;
        [SerializeField] private Material _selectedMaterial = default;
        [SerializeField] private Renderer _renderer = default;

        public virtual Transform SpawnTransform => _spawnTransform;

        private bool _highlited = false;
        private Material _defaultMaterial;

        private void OnEnable()
        {
            _defaultMaterial = _renderer.material;
            BuildingManager.TowerSelected += TowerSelected;
            BuildingManager.TowerUnSelected += TowerUnSelected;
        }
        
        private void OnDisable()
        {
            BuildingManager.TowerSelected -= TowerSelected;
            BuildingManager.TowerUnSelected -= TowerUnSelected;
        }

        private void OnMouseEnter()
        {
            Select();
        }

        private void OnMouseExit()
        {
            UnSelect();
        }

        private void OnMouseDown()
        {
            TileSelected.Invoke(this);
            gameObject.SetActive(false);
        }
        
        private void TowerUnSelected()
        {
            UnHighlight();
        }

        private void TowerSelected()
        {
            Highlight();
        }

        private void Highlight()
        {
            _highlited = true;
            _selectedParticleSystem.Play();
            _renderer.material = _highlightMaterial;
        }

        private void UnHighlight()
        {
            _highlited = false;
            _selectedParticleSystem.Stop();
            _renderer.material = _defaultMaterial;
        }

        private void Select()
        {
            if (_highlited)
            {
                _renderer.material = _selectedMaterial;
            }
        }

        private void UnSelect()
        {
            if (_highlited)
            {
                _renderer.material = _highlightMaterial;
            }
        }
    }
}