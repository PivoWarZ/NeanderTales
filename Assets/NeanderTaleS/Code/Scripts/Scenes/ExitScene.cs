using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Scenes
{
    public sealed class ExitScene: MonoBehaviour
    {
        [SerializeField] private TMP_Text _infoPopup;
        [SerializeField] string  _nextScene;
        private Transform _projectContextTransform;
        private GameObject _player = null;
        private bool _isExit;

        private void Start()
        {
            _projectContextTransform = FindFirstObjectByType<ProjectContext>().transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_player)
            {
                other.gameObject.TryGetComponent<Player>(out Player player);
                
                if (player)
                {
                    _player = player.gameObject;
                }
            }
            
            _infoPopup.text = $"Press E to go {_nextScene}";
            _infoPopup.transform.parent.gameObject.SetActive(true);
            _isExit = true;

        }

        private void OnTriggerExit(Collider other)
        {
            _infoPopup.transform.parent.gameObject.SetActive(false);
            _isExit = false;
        }

        private void Update()
        {
            if (!_isExit)
            {
                return;
            }

            if (Input.GetKey(KeyCode.E))
            {
                _player.gameObject.transform.SetParent(_projectContextTransform);
                SceneManager.LoadScene(_nextScene);
            }
        }
    }
}