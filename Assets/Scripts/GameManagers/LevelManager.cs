using System;
using System.Collections.Generic;
using GamePlay.Towers;
using UnityEngine;
using Util;

namespace GameController
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<LevelController> _levels = new List<LevelController>();

        private int _nextLevel = 0;
        private LevelController _currentLevelController;
        
        private void OnEnable()
        {
            GameManager.GameStarted += GameManagerOnGameStarted;
            GameManager.GameEnded += GameManagerOnGameEnded;
        }
        
        private void OnDisable()
        {
            GameManager.GameStarted -= GameManagerOnGameStarted;
            GameManager.GameEnded -= GameManagerOnGameEnded;
        }
        

        private void GameManagerOnGameEnded()
        {
            GenerateNextLevel();
        }

        private void GameManagerOnGameStarted()
        {
            if (_nextLevel == 0)
            {
                GenerateNextLevel();
            }
            _currentLevelController.StartLevel();
        }

        private void GenerateNextLevel()
        {
            if (_currentLevelController != null)
            {
                Destroy(_currentLevelController);
            }

            if (_nextLevel == _levels.Count)
            {
                _nextLevel = 0;
            }

            _currentLevelController = Instantiate(_levels[_nextLevel]);
        }
    }
}