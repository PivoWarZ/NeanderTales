using NeanderTaleS.Code.Scripts.Systems.SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Scenes
{
    public class SaveArea: MonoBehaviour
    {
        [SerializeField] private TMP_Text _infoPopup;
        private SaveLoadManager _saveLoadManager;
        private bool _isTriggerEnter;
        
        [Inject]
        public void Construct(SaveLoadManager manager)
        {
            _saveLoadManager = manager;
        }

        private async void OnTriggerEnter(Collider other)
        {
            _infoPopup.text = $"Press E to SaveGame";
            _infoPopup.transform.parent.gameObject.SetActive(true);
            _isTriggerEnter = true;

        }

        private void OnTriggerExit(Collider other)
        {
            _infoPopup.transform.parent.gameObject.SetActive(false);
            _isTriggerEnter = false;
        }

        private void Update()
        {
            if (!_isTriggerEnter)
            {
                return;
            }

            if (Input.GetKey(KeyCode.E))
            {
                _saveLoadManager.SaveGame();
            }
        }
    }
}