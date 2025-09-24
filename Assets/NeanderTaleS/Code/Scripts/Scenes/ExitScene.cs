using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NeanderTaleS.Code.Scripts.Scenes
{
    public class ExitScene: MonoBehaviour
    {
        [SerializeField] private GameObject _quitText;
        [SerializeField] string  _nextScene;
        private bool _isExit;

        private void Awake()
        {
            _quitText.SetActive(false);
        }

        private async void OnTriggerEnter(Collider other)
        {
            _quitText.SetActive(true);
            _isExit = true;

        }

        private void OnTriggerExit(Collider other)
        {
            _quitText.SetActive(false);
            _isExit = false;
        }

        private void Update()
        {
            if (!_isExit)
            {
                return;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene(_nextScene);
            }
        }
    }
}