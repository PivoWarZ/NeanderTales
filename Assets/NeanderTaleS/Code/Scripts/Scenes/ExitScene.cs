using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NeanderTaleS.Code.Scripts.Scenes
{
    public class ExitScene: MonoBehaviour
    {
        [SerializeField] private TMP_Text _infoPopup;
        [SerializeField] string  _nextScene;
        private bool _isExit;

        private void OnTriggerEnter(Collider other)
        {
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
                SceneManager.LoadScene(_nextScene);
            }
        }
    }
}