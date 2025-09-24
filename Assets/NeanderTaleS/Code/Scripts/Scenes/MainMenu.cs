using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.Scenes
{
    public class MainMenu: MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _loadGameButton;
        [SerializeField] private string _nextSceneName;
        private void Awake()
        {
            _startGameButton.onClick.AddListener(StartGame);
            _loadGameButton.onClick.AddListener(LoadGame);
        }

        private void LoadGame()
        {
            Debug.Log("Loading Game");
        }

        private void StartGame()
        {
            SceneManager.LoadScene(_nextSceneName);
        }
    }
}