using System;
using NeanderTaleS.Code.Scripts.Systems.GameCycle;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Scenes
{
    public class MainMenu: MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _loadGameButton;
        [SerializeField] private string _nextSceneName;
        private GameLoader _gameLoader;
        private GameCycleManager _cycleManager;

        [Inject]
        public void Construct(GameLoader gameLoader, GameCycleManager cycleManager)
        {
            _gameLoader = gameLoader;
            _cycleManager = cycleManager;
        }

        private void Awake()
        {
            _startGameButton.onClick.AddListener(StartGame);
            _loadGameButton.onClick.AddListener(LoadGame);
        }

        private void LoadGame()
        {
            _gameLoader.LoadGame();
        }

        private void StartGame()
        {
            SceneManager.LoadScene(_nextSceneName);
            _cycleManager.StartGame();
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(StartGame);
            _loadGameButton.onClick.RemoveListener(LoadGame);
        }
    }
}