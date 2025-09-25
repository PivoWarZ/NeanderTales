using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad;
using TMPro;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Scenes
{
    public class SaveArea: MonoBehaviour
    {
        [SerializeField] private TMP_Text _infoPopup;
        private SaveLoadManager _saveLoadManager;
        private bool _isTriggerEnter;
        private ITakeDamageable _hitPoints;
        
        [Inject]
        public void Construct(SaveLoadManager manager, PlayerService playerService)
        {
            _saveLoadManager = manager;
            _hitPoints = playerService.GetPlayer().GetComponent<ITakeDamageable>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _infoPopup.text = $"Press E to SaveGame";
            _infoPopup.transform.parent.gameObject.SetActive(true);
            
            var currentPoints = _hitPoints.CurrentHitPoints.CurrentValue;
            var maxPoints = _hitPoints.MaxHitPoints.CurrentValue;

            if (currentPoints < maxPoints)
            {
                _hitPoints.AddedtHitPoints(maxPoints - currentPoints);
            }

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