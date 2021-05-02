using System;
using DG.Tweening;
using GamePlay;
using Types;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuildButtonController : MonoBehaviour
    {
        public static event Action<TowerType> Pressed = delegate {  };
        
        [SerializeField] private TowerType _type = TowerType.GunTower;
        [SerializeField] private Vector3 _selectedScale = new Vector3(4, 4, 4);
        [SerializeField] private Vector3 _selectedPosDelta = new Vector3(0, 20, 0);
        [SerializeField] private float _selectionSpeed = 0.1f;

        private Vector3 _startPos;
        private Vector3 _startScale;

        private Transform _transform;
            
        private void OnEnable()
        {
            _transform = transform;
            _startPos = _transform.position;
            _startScale = _transform.localScale;
            InputController.Pressed += InputControllerOnPressed;
            BuildingManager.TowerUnSelected += TowerUnSelected;
            Pressed += InputControllerOnPressed;
        }

        private void OnDisable()
        {
            InputController.Pressed -= InputControllerOnPressed;
            BuildingManager.TowerUnSelected -= TowerUnSelected;
            Pressed -= InputControllerOnPressed; 
        }
        
        public void Action()
        {
            Pressed.Invoke(_type);
        }
        
        private void TowerUnSelected()
        {
            UnSelect();
        }
        
        private void InputControllerOnPressed(TowerType type)
        {
            if (type == _type)
            {
                Select();
            }
            else
            {
                UnSelect();
            }
        }
        
        private void Select()
        {
            DOTween.Sequence().Append(_transform.DOMove(_startPos + _selectedPosDelta, _selectionSpeed))
                .Join(_transform.DOScale(_selectedScale, _selectionSpeed));
        }

        private void UnSelect()
        {
            DOTween.Sequence().Append(_transform.DOMove(_startPos, _selectionSpeed))
                .Join(_transform.DOScale(_startScale, _selectionSpeed));
        }
    }
}